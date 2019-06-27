using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    bool active = true;
    private Vector3 velocity = Vector3.zero;
    public float rotSpeed;

    void Update()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 temp = Input.mousePosition;
        temp.z = 23.5f; // Set this to be the distance you want the object to be placed in front of the camera.
        this.transform.position = Vector3.SmoothDamp(transform.position, Camera.main.ScreenToWorldPoint(temp),ref velocity, rotSpeed);
    }
}
