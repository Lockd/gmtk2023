using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    private bool isMainCamera = true;
    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private CinemachineVirtualCamera cam2;

    private static CameraSwitcher _instance;
    public static CameraSwitcher Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }

    public void SwitchPriority(Transform lookAtAfterSwitch)
    {
        if (isMainCamera)
        {
            cam1.Priority = 0;
            cam2.Priority = 1;
            cam2.LookAt = lookAtAfterSwitch;
            cam2.Follow = lookAtAfterSwitch;
        }
        else
        {
            cam1.Priority = 1;
            cam2.Priority = 0;
            cam1.LookAt = lookAtAfterSwitch;
            cam1.Follow = lookAtAfterSwitch;
        }

        isMainCamera = !isMainCamera;
    }

    public void SetLookAt(Transform lookAt, Transform lookAtAfterSwitch)
    {
        CinemachineVirtualCamera inactiveCamera = cam1.Priority > 0 ? cam1 : cam2;
        inactiveCamera.LookAt = lookAt;
        inactiveCamera.Follow = lookAt;
        SwitchPriority(lookAtAfterSwitch);
    }
}
