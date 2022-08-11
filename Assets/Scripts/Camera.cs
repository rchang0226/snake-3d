using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    private Vector3 point;
    private Vector3 direction = Vector3.zero;
    public float speed;

    void Start()
    {
        point = target.position;
        transform.LookAt(point);
    }

    void Update()
    {
        if (Input.GetButton("Fire1")) {
            direction = Vector3.up * Input.GetAxis("Mouse X");
            direction += -transform.right.normalized * Input.GetAxis("Mouse Y");
            transform.RotateAround(point, direction, speed);
        }
    }
}
