using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimEvnet : MonoBehaviour
{
    public UnityAction attackEvent;
    public UnityAction attackStart;
    public UnityAction attackEnd;

    public void AttackCheck()
    {
        if(attackEvent != null)
        {
            attackEvent.Invoke();
        }
    }
}
