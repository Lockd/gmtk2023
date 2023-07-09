using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followTarget;
    public float moveSpeed;
    public static bool cExists;
    private Vector3 roomPos;
    private static int cameraState;

    public Vector3 offset;

    public float damping;

    private Vector3 vel = Vector3.zero;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movePos = followTarget.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePos, ref vel, damping);
    }
}
