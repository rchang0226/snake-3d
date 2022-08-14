using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonView : MonoBehaviour
{

    public float sensitivity;
    public float yRotationLimit;
    const string xAxis = "Mouse X";
    const string yAxis = "Mouse Y";
    Vector2 rotation = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotation.x += Input.GetAxis(xAxis) * sensitivity;
        rotation.y += Input.GetAxis(yAxis) * sensitivity;
        rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        transform.localRotation = xQuat * yQuat;
    }
}
