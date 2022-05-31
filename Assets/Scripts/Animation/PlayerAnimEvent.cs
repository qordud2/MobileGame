using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimEvent : MonoBehaviour
{
    public UnityAction attackStartAction;
    public UnityAction attackEndAction;
    public UnityAction skillStartAction;
    public UnityAction skillEndAction;
    public UnityAction<float, float, float> skillAttackCheck;
    public UnityAction<float, float, float> skill2AttackCheck;

    public GameObject greatSword;
    
    public void SkillAttackCheck()
    {
        skillAttackCheck?.Invoke(3, 0, 150.0f);
    }

    public void Skill2AttackCheck()
    {
        skill2AttackCheck?.Invoke(2.5f, 0, 120.0f);
    }
}
