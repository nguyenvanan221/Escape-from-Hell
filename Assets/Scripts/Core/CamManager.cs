using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public static CamManager Instance { get; private set; }
    [HideInInspector]
    public CinemachineVirtualCamera virtualCamera;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        GameObject vCameraObject = GameObject.FindWithTag("VirtualCamera");
        virtualCamera = vCameraObject.GetComponent<CinemachineVirtualCamera>();
    }
}
