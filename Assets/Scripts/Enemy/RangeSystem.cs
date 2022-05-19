using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RangeSystem : MonoBehaviour
{
    public UnityAction detectPlayerAction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.gameObject.tag == "Player")
        {
            detectPlayerAction.Invoke();
        }
    }
}
