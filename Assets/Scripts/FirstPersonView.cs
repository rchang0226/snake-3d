using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView : MonoBehaviour
{

    public float sensitivity;
    public float xRotationLimit;
    public float yRotationLimit;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";
    Vector2 rotation = Vector2.zero;
    Camera _camera;
    private Vector3 direction;
    private Vector3 axis;

    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_camera.enabled) {
            var direction = this.transform.parent.GetComponent<Snake>().direction;
            var axis = this.transform.parent.GetComponent<Snake>().axis;

            rotation.x += Input.GetAxis(xAxis) * sensitivity;
            rotation.x = Mathf.Clamp(rotation.x, -xRotationLimit, xRotationLimit);
            rotation.y += Input.GetAxis(yAxis) * sensitivity;
            rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
            var xQuat = Quaternion.AngleAxis(rotation.x, axis);
            var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.Cross(direction, axis));

            transform.localRotation = xQuat * yQuat;
        } 
    }
}
