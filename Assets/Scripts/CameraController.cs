using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public float sensitivity;


    // Start is called before the first frame update
    void Start()
    {
        firstPersonCamera.enabled = false;
        thirdPersonCamera.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle between first and third person view
        if (Input.GetButtonDown("Jump")) {
            ToggleView();
        }
    }

    private void ToggleView() {
        firstPersonCamera.enabled = !firstPersonCamera.enabled;
        thirdPersonCamera.enabled = !thirdPersonCamera.enabled;
    }
}
