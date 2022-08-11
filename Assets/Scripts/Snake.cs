using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector3 direction;
    private Vector3 targetTile;
    private Vector3 currTile;
    public float speed;
    private Vector3 axis;
    private Transform hat;


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
        if (Input.GetAxis("Horizontal") < 0 && Input.anyKeyDown) {
            direction = Quaternion.AngleAxis(-90, axis) * direction;
        }
        else if (Input.GetAxis("Horizontal") > 0 && Input.anyKeyDown) {
            direction = Quaternion.AngleAxis(90, axis) * direction;
        }
        else if (Input.GetAxis("Vertical") < 0 && Input.anyKeyDown) {
            (direction, axis) = (-axis, direction);
        }
        else if (Input.GetAxis("Vertical") > 0 && Input.anyKeyDown) {
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

}
