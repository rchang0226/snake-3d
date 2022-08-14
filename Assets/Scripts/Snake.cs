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
    private List<Transform> segments; // the snake body segments
    public Transform segmentPrefab;


    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.forward;
        targetTile = transform.position;
        axis = Vector3.up;
        hat = transform.GetChild(0);

        segments = new List<Transform>();
        segments.Add(this.transform); // the head is the first segment

    }

    // Update is called once per frame
    void Update()
    {
        // Control movement of snake
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
            // update tail position
            for (int i = segments.Count -1; i > 0; i--) {
                segments[i].position = segments[i-1].position;
            }

            // update head position
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

    private void Grow() {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void OnTriggerEnter(Collider obj) {
        if (obj.tag == "Food") {
            Grow();
        }
    }

}
