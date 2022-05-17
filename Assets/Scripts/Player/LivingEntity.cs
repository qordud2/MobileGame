using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour
{
    [HideInInspector]
    public float startHelath = 200.0f;
    public float health { get; protected set; }
    float startMana = 200.0f;
    public float mana { get; protected set; }
    public bool dead { get; protected set; }
    public UnityAction onDeath;

    public virtual void OnEnable()
    {
        dead = false;
        health = startHelath;
        mana = startMana;
    }

    public virtual void OnDamage(float damage)
    {
        health -= damage;

        if(health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual bool OnUseMana(float useMana)
   {
        bool use = true;
        if(mana - useMana < 0)
        {
            use = false;
            return use;
        }
        mana -= useMana;

        return use;
   }

    public virtual void Die()
    {
        if(onDeath != null)
        {
            onDeath.Invoke();
        }

        dead = true;
    }
}
