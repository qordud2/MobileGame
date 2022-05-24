using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG
{
    public class Enemy : LivingEntity
    {
        public RangeSystem rangeSystem;
        public Animator animator;
        public NavMeshAgent navmesh;

        PlayerHealth player;
        EnemyAnimEvnet animEvnet;

        Vector3 initPosition;
        float romingTime = 0.0f;
        float attackTime = 0.0f;
        float skillTime = 1.0f;
        float attackDamage = 30.0f;

        public enum EnemyState
        {
            Normal, Roming, Attack, Skill
        }

        public EnemyState enemyState = EnemyState.Normal;

        private void Awake()
        {
            rangeSystem = GetComponentInChildren<RangeSystem>();
            animator = GetComponentInChildren<Animator>();
            navmesh = GetComponent<NavMeshAgent>();
            animEvnet = GetComponentInChildren<EnemyAnimEvnet>();

            startHelath = 200.0f;
        }

        private void Start()
        {
            initPosition = transform.position;
            rangeSystem.detectPlayerAction += DetectPlayer;
            animEvnet.attackEvent += AttackCheck;
            animEvnet.attackStart += NavStop;
            animEvnet.attackEnd += NavReMode;
        }

        private void Update()
        {
            StateProcess();
        }

        public void ChangeState(EnemyState s)
        {
            if (enemyState == s) return;
            enemyState = s;

            switch (enemyState)
            {
                case EnemyState.Normal:
                    {
                        animator.SetFloat("Speed", 0.0f);
                        navmesh.isStopped = true;
                        romingTime = 0.0f;
                        break;
                    }
                case EnemyState.Roming:
                    {
                        Vector3 radiusPoint = Random.onUnitSphere;
                        radiusPoint.y = 0.0f;
                        int radiusLength = 10;
                        Vector3 desPosition = initPosition + radiusPoint * radiusLength;
                        navmesh.isStopped = false;
                        navmesh.SetDestination(desPosition);
                        break;
                    }
                case EnemyState.Attack:
                    {
                        player = rangeSystem.player.GetComponent<PlayerHealth>();
                        navmesh.isStopped = false;
                        navmesh.stoppingDistance = 3.0f;

                        break;
                    }
                case EnemyState.Skill:
                    {
                        navmesh.isStopped = false;
                        navmesh.stoppingDistance = 5.0f;
                        player = rangeSystem.player.GetComponent<PlayerHealth>();
                        break;
                    }
            }
        }

        void StateProcess()
        {
            switch (enemyState)
            {
                case EnemyState.Normal:
                    {
                        romingTime += Time.deltaTime;
                        if (romingTime > 2.0f)
                        {
                            ChangeState(EnemyState.Roming);
                        }
                        break;
                    }
                case EnemyState.Roming:
                    {
                        animator.SetFloat("Speed", navmesh.velocity.magnitude / navmesh.speed);
                        if (navmesh.remainingDistance < navmesh.stoppingDistance)
                        {
                            ChangeState(EnemyState.Normal);
                        }

                        break;
                    }
                case EnemyState.Attack:
                    {
                        if (player != null)
                        {
                            Vector3 dir = player.transform.position - this.transform.position;
                            dir.y = 0;
                            dir.Normalize();
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 10.0f);

                            animator.SetFloat("Speed", navmesh.velocity.magnitude / navmesh.speed);
                            navmesh.SetDestination(player.transform.position);

                            float distance = Vector3.Distance(transform.position, player.transform.position);
                            if (distance >= 20.0f)
                            {
                                ChangeState(EnemyState.Normal);
                            }

                            if(attackTime > Mathf.Epsilon)
                            {
                                attackTime -= Time.deltaTime;
                            }
                            else
                            {
                                OnAttack();
                            }
                        }
                        break;
                    }
                case EnemyState.Skill:
                    {
                        if (player != null)
                        {
                            Vector3 dir = player.transform.position - this.transform.position;
                            dir.y = 0;
                            dir.Normalize();
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 10.0f);

                            animator.SetFloat("Speed", navmesh.velocity.magnitude / navmesh.speed);
                            navmesh.SetDestination(player.transform.position);

                            float distance = Vector3.Distance(transform.position, player.transform.position);
                            if (distance >= 20.0f)
                            {
                                ChangeState(EnemyState.Normal);
                            }

                            if (skillTime > Mathf.Epsilon)
                            {
                                skillTime -= Time.deltaTime;
                            }
                            else
                            {
                                OnSkill();
                            }
                        }

                            break;
                    }
            }
        }

        void OnAttack()
        {
            if (navmesh.remainingDistance <= navmesh.stoppingDistance && !dead && enemyState == EnemyState.Attack)
            {
                float random = Random.Range(0, 10);
                if (random > 3)
                {
                    animator.SetTrigger("Attack");
                }
                else
                {
                    animator.SetTrigger("Attack2");
                }
                ChangeState(EnemyState.Normal);
                attackTime = 2.0f;
            }
        }

        void OnSkill()
        {
            if (navmesh.remainingDistance <= navmesh.stoppingDistance && !dead && enemyState == EnemyState.Skill)
            {
                animator.SetTrigger("Skill");
                skillTime = 5.0f;
                ChangeState(EnemyState.Normal);
            }
        }

        void AttackCheck()
        {
            if(player != null)
            {
                float dot = Vector3.Dot(transform.position, player.transform.position);
                float distnace = Vector3.Distance(transform.position, player.transform.position);
                if(dot >= 0 && distnace <= navmesh.stoppingDistance)
                {
                    player.OnDamage(attackDamage);
                    Debug.Log("Enemy Attack");
                }
            }
        }

        void NavStop()
        {
            navmesh.isStopped = true;
        }

        void NavReMode()
        {
            navmesh.isStopped = false;
        }

        public void Setup(float newHealth)
        {
            startHelath = newHealth;
        }


        public override void OnDamage(float damage)
        {
            base.OnDamage(damage);
            Debug.Log("health : " + health);
        }

        public override void Die()
        {
            base.Die();
            Debug.Log("Eneemy Die");
        }

        void DetectPlayer()
        {
            float random = Random.Range(0, 10);
            if(random > 9)
            {
                ChangeState(EnemyState.Attack);
            }
            else
            {
                ChangeState(EnemyState.Skill);
            }
            
        }


    }
}

