using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    Camera mainCamera;

    void Awake()
    {
        mainCamera = GameObject.Find("Camera").GetComponent<Camera>();
    }

    void Update()
    {
        transform.LookAt(mainCamera.transform, Vector3.up);
    }
}
