using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public enum EnemyState
    {
        Normal, Roming, Attack, Skill
    }
    public enum EnemyKind
    {
        BlackKnight, Lizard, Worm,
    }


    public class Enemys : LivingEntity
    {
        public EnemyState enemyState = EnemyState.Normal;
        public EnemyKind enemyKind = EnemyKind.BlackKnight;

        protected virtual void DetectPlayer()
        {
            ChangeState(EnemyState.Attack);
        }

        protected virtual void ChangeState(EnemyState s)
        {
            if (enemyState == s) return;
            enemyState = s;
        }

        protected virtual void Setup(EnemyKind kind, float startHp, float hp)
        {
            enemyKind = kind;
            startHelath = startHp;
            health = hp;
        }
    }
}