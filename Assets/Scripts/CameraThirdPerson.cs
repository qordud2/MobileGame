using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraThirdPerson : MonoBehaviour
{
    [SerializeField]
    Transform avatarRoot;
    [SerializeField]
    Transform lookTarget;
    public CinemachineVirtualCamera cinemachineVirtual;

    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtual.Follow = avatarRoot;
        cinemachineVirtual.LookAt = lookTarget;
    }
}
