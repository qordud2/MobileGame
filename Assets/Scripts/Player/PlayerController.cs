using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public class PlayerController : MonoBehaviour
    {
        PlayerAttack playerAttack;

        // Start is called before the first frame update
        void Awake()
        {
            playerAttack = GetComponentInChildren<PlayerAttack>();
        }

        // Update is called once per frame
        void Update()
        {
            //Attack();
        }

        void Attack()
        {
            //if(InputManager.Instance.attack)
            //{
            //    playerAttack.Attack();
            //}
        }
    }
}


