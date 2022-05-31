using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RPG
{
    public class WeaponTrigger : MonoBehaviour
    {
        public UnityAction<Vector3, Vector3> AttackEvent;
        [HideInInspector]
        public Enemys enemy;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.gameObject.tag == "Enemy")
            {
                enemy = other.transform.gameObject.GetComponent<Enemys>();
                Vector3 hitPoint = other.ClosestPoint(transform.position);
                Vector3 hitNormal = transform.position - other.transform.position;
                AttackEvent?.Invoke(hitPoint, hitNormal);
                Debug.Log("Trigger");
            }
        }
    }
}

