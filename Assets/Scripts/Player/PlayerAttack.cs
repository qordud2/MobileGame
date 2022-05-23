using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    public class PlayerAttack : MonoBehaviour
    {

        public enum AttackState
        {
            Normal, Attack, Skill, Skill2,
        }
        public AttackState attackState = AttackState.Normal;


        public GameObject weponObject;
        WeaponTrigger weaponTrigger;
        Animator animator;
        [HideInInspector]
        public PlayerAnimEvent animEvent;

        float AttackDamage = 80.0f;
        float attackTime = 0.0f;
        float attackWaitTime = 0.0f;
        float skillTime = 0.0f;
        float skill2Time = 0.0f;
        float skillDamage = 150.0f;
        float skill2Damage = 120.0f;

        void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            animEvent = animator.GetComponent<PlayerAnimEvent>();
            weaponTrigger = weponObject.GetComponent<WeaponTrigger>();
        }

        private void Start()
        {
            weaponTrigger.AttackEvent += OnAttack;
        }

        private void Update()
        {
            StateProcess();

        }

        public void ChangeState(AttackState s)
        {
            if (attackState == s) return;
            attackState = s;

            switch (attackState)
            {
                case AttackState.Normal:
                    {
                        if (attackTime > Mathf.Epsilon)
                        {
                            attackWaitTime = attackTime + 0.5f;
                        }
                        break;
                    }
                case AttackState.Attack:
                    {
                        break;
                    }
                case AttackState.Skill:
                    {
                        break;
                    }
                case AttackState.Skill2:
                    {
                        break;
                    }
            }
        }

        private void StateProcess()
        {
            switch (attackState)
            {
                case AttackState.Normal:
                    {
                        if (InputManager.Instance.attack)
                        {
                            ChangeState(AttackState.Attack);
                        }

                        if (skillTime > Mathf.Epsilon)
                        {
                            skillTime -= Time.deltaTime;
                        }
                        else
                        {
                            if (InputManager.Instance.skill)
                            {
                                ChangeState(AttackState.Skill);
                            }
                        }

                        if(skill2Time > Mathf.Epsilon)
                        {
                            skill2Time -= Time.deltaTime;
                        }
                        else
                        {
                            if (InputManager.Instance.skill2)
                            {
                                ChangeState(AttackState.Skill2);
                            }

                        }

                        

                        if (animator.GetBool("Attacking"))
                        {
                            if (attackWaitTime > Mathf.Epsilon)
                            {
                                attackWaitTime -= Time.deltaTime;
                            }
                            else
                            {
                                AttackEnd();
                            }
                        }

                        break;
                    }
                case AttackState.Attack:
                    {
                        if (attackTime > Mathf.Epsilon)
                        {
                            attackTime -= Time.deltaTime;
                        }
                        else
                        {
                            Attack();
                        }
                        break;
                    }
                case AttackState.Skill:
                    {
                        Skill();
                        break;
                    }
                case AttackState.Skill2:
                    {
                        Skill2();
                        break;
                    }
            }
        }

        public void Attack()
        {
            if(animEvent.attackStartAction != null)
            {
                animEvent.attackStartAction.Invoke();
            }

            animator.SetBool("Attacking", true);
            animator.SetTrigger("Attack");

            attackTime = 0.3f;
            ChangeState(AttackState.Normal);
        }

        void AttackEnd()
        {
            animator.SetTrigger("AttackExit");
            animator.SetBool("Attacking", false);
            if(animEvent.attackEndAction != null)
            {
                animEvent.attackEndAction.Invoke();
            }
        }

        void OnAttack()
        {
            Enemy attackEnemy = weaponTrigger.enemy;
            if (attackEnemy != null)
            {
                attackEnemy.OnDamage(AttackDamage);
                AttackEnd();
            }
        }

        void Skill()
        {
            if (animEvent.skillStartAction != null)
            {
                animEvent.skillStartAction.Invoke();
            }

            animator.SetTrigger("SkillAttack");
            skillTime = 5.0f;
            SkillArea(3, 0, skillDamage);
            ChangeState(AttackState.Normal);
        }

        void Skill2()
        {
            if(animEvent.skillStartAction != null)
            {
                animEvent.skillStartAction.Invoke();
            }

            animator.SetTrigger("SkillAttack2");
            skill2Time = 3.0f;
            SkillArea(1.5f, 0, skill2Damage);
            ChangeState(AttackState.Normal);
        }

        void SkillArea(float radius, float maxDistance, float damage)
        {
            RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, radius, Vector3.up, maxDistance);

            foreach(RaycastHit hit in rayHits)
            {
                if(hit.transform.gameObject.tag == "Enemy")
                {
                    Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    if(enemy != null)
                    {
                        Debug.Log("EnemyHit");
                        enemy.OnDamage(damage);
                    }
                }
            }
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawSphere(transform.position, 5);
        //}
    }

}
