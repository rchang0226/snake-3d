using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    public Vector3 direction;
    private Vector3 targetTile;
    private Vector3 currTile;
    public float speed;
    public Vector3 axis;
    private Transform hat;
    private Vector3 rotation;
    public float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.forward;
        targetTile = transform.position;
        axis = Vector3.up;
        hat = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Control movement of snake
        if (Input.GetAxis("Horizontal") < 0 && Input.anyKeyDown) {
            direction = Quaternion.AngleAxis(-90, axis) * direction;
            rotation = axis * -90;
        }
        else if (Input.GetAxis("Horizontal") > 0 && Input.anyKeyDown) {
            direction = Quaternion.AngleAxis(90, axis) * direction;
            rotation = axis * 90;
        }
        else if (Input.GetAxis("Vertical") < 0 && Input.anyKeyDown) {
            rotation = Vector3.Cross(direction, axis) * -90;
            (direction, axis) = (-axis, direction);
        }
        else if (Input.GetAxis("Vertical") > 0 && Input.anyKeyDown) {
            rotation = Vector3.Cross(direction, axis) * 90;
            (direction, axis) = (axis, -direction);
        }
    }

    void FixedUpdate() {
        currTile = new Vector3 (
        Mathf.Round(transform.position.x),
        Mathf.Round(transform.position.y),
        Mathf.Round(transform.position.z));

        if (targetTile.Equals(transform.position)) {
            targetTile = currTile + direction;
            StartCoroutine(SmoothLerp(1/speed));
            hat.transform.localPosition = axis * 0.5f;
            StartCoroutine(SmoothRotation(1/rotationSpeed));
            rotation = Vector3.zero;
        }
    }

    // SmoothLerp code repurposed from https://answers.unity.com/questions/1501234/smooth-forward-movement-with-a-coroutine.html
    private IEnumerator SmoothLerp (float time) {
        Vector3 startingPos = currTile;
        Vector3 finalPos = targetTile;

        float elapsedTime = 0;

        while (elapsedTime < time) {
            transform.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime/time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = finalPos;
    }

    private IEnumerator SmoothRotation (float time) {
        Vector3 startingRot = transform.eulerAngles;
        Vector3 finalRot = startingRot + rotation;

        float elapsedTime = 0;

        while (elapsedTime < time) {
            transform.eulerAngles = Vector3.Lerp(startingRot, finalRot, (elapsedTime/time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.eulerAngles = finalRot;
    }

}
