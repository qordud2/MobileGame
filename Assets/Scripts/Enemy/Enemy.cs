using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    private void Awake()
    {
        startHelath = 200.0f;
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



}
