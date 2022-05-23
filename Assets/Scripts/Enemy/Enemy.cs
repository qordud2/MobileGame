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

        Vector3 initPosition;
        float romingTime = 0.0f;

        public enum EnemyState
        {
            Normal, Roming, Attack, Skill1
        }

        EnemyState enemyState = EnemyState.Normal;

        private void Awake()
        {
            rangeSystem = GetComponentInChildren<RangeSystem>();
            animator = GetComponentInChildren<Animator>();
            navmesh = GetComponent<NavMeshAgent>();

            startHelath = 200.0f;
        }

        private void Start()
        {
            initPosition = transform.position;
            rangeSystem.detectPlayerAction += DetectPlayer;
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

                        break;
                    }
                case EnemyState.Skill1:
                    {
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

                            if (navmesh.remainingDistance <= navmesh.stoppingDistance)
                            {
                                OnAttack();
                            }

                            
                        }

                        break;
                    }
                case EnemyState.Skill1:
                    {
                        break;
                    }
            }
        }

        void OnAttack()
        {
            animator.SetTrigger("Attack");
        }

        void AttackCheck()
        {
                
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
            ChangeState(EnemyState.Attack);
        }


    }
}

