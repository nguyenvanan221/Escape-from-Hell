using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : Singleton<CamManager>
{
    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    void Awake()
    {
        GameObject vCameraObject = GameObject.FindWithTag("VirtualCamera");
        virtualCamera = vCameraObject.GetComponent<CinemachineVirtualCamera>();
    }
}
