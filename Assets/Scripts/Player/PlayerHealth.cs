using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : LivingEntity
{
    Animator animator;
    public UnityAction reactOnDamaged;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal, bool react)
    {
        if(react)
        {
            animator.ResetTrigger("Damaged");
            animator.SetTrigger("Damaged");
            reactOnDamaged?.Invoke();
        }

        base.OnDamage(damage, hitPoint, hitNormal, react);
    }

    public override bool OnUseMana(float useMana)
    {
        return base.OnUseMana(useMana);
    }

}
