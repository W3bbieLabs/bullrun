using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Camera mainCam;

    public Vector3 offset;

    void Start()
    {
        mainCam = Camera.main;
    }

    private void LateUpdate()
    {
        mainCam.transform.position = transform.position + offset;
    }
}