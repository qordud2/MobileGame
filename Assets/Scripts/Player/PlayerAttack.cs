using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttack : MonoBehaviour
{
    float AttackDamage = 100.0f;
    public enum AttackState
    {
        Normal, Attack,
    }
    AttackState attackState = AttackState.Normal;

    
    public GameObject weponObject;
    WeaponTrigger weaponTrigger;
    Animator animator;
    int atkCount = 1;
    int maxAtkCount = 5;
    float attackTime = 0.0f;

    public UnityAction attackAction;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
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

        switch(attackState)
        {
            case AttackState.Normal:
                {
                    break;
                }
            case AttackState.Attack:
                {
                    break;
                }
        }
    }    

    private void StateProcess()
    {
        switch(attackState)
        {
            case AttackState.Normal:
                {
                    if (InputManager.Instance.attack)
                    {
                        ChangeState(AttackState.Attack);
                    }
                    break;
                }
            case AttackState.Attack:
                {
                    if(attackTime > Mathf.Epsilon)
                    {
                        attackTime -= Time.unscaledDeltaTime;
                    }
                    else
                    {
                        Attack();
                        attackAction.Invoke();
                    }
                    break;
                }
        }
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        animator.SetFloat("AttackCount", atkCount);
        atkCount++;
        if(atkCount > maxAtkCount)
        {
            atkCount = 1;
        }
        attackTime = 0.5f;
        ChangeState(AttackState.Normal);
    }

    void OnAttack()
    {
        Enemy attackEnemy = weaponTrigger.enemy;
        attackEnemy.OnDamage(AttackDamage);
    }
}
