using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target;
    private Vector3 point;
    private Vector3 direction = Vector3.zero;

    void Start()
    {
        point = target.position;
        transform.LookAt(point);
    }

    void Update()
    {
        if ((Input.GetKey(KeyCode.A))) {
            direction = Vector3.up;
        }
        else if (Input.GetKey(KeyCode.D)) {
            direction = Vector3.down;
        }
        else if (Input.GetKey(KeyCode.S)) {
            direction = -transform.right.normalized;
        }
        else if (Input.GetKey(KeyCode.W)) {
            direction = transform.right.normalized;
        }
        else {
            direction = Vector3.zero;
        }
        transform.RotateAround(point, direction, 0.5f);
    }
}
