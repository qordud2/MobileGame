using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeSystem : MonoBehaviour
{
    public UnityAction detectPlayerAction;
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.tag == "Player")
        {
            player = other.transform.gameObject;
            detectPlayerAction?.Invoke();
        }
    }
}
