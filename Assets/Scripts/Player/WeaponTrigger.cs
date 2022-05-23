using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    public class WeaponTrigger : MonoBehaviour
    {
        public UnityAction AttackEvent;
        [HideInInspector]
        public Enemy enemy;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.gameObject.tag == "Enemy")
            {
                enemy = other.transform.gameObject.GetComponent<Enemy>();
                AttackEvent.Invoke();
                Debug.Log("Trigger");
            }
        }
    }
}

