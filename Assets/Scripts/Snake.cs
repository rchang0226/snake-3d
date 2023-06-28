using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

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
    private bool reset;
    private bool grow;
    public int initialSize;
    private int sizeCounter; // to slowly instantiate the initial size
    public Transform food;


    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.right;
        targetTile = transform.position;
        axis = Vector3.up;
        hat = transform.GetChild(0);

        segments = new List<Transform>();
        segments.Add(this.transform); // the head is the first segment

        reset = false;
        grow = false;

        sizeCounter = initialSize;

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

            for (int i = 1; i < segments.Count; i++) {
                segments[i].GetComponent<BoxCollider>().enabled = true;
            }

            if (sizeCounter > 0) {
                Grow();
                sizeCounter--;
            }

            if (grow) {
                Grow();
                grow = false;
            }

            // update tail position
            for (int i = segments.Count -1; i > 0; i--) {
                StartCoroutine(SmoothLerp(1/speed, segments[i], segments[i].position, segments[i-1].position));
            }

           // update head position
            targetTile = currTile + direction;
            StartCoroutine(SmoothLerp(1/speed, transform, currTile, targetTile));
            hat.transform.localPosition = axis * 0.5f;

            if (reset) {
                StopAllCoroutines();
                ResetState();
                reset = false;
            }
        }
    }

    void OnEpisodeBegin() {
        StopAllCoroutines();
        ResetState();
        reset = false;
    }

    void CollectObserviations(VectorSensor sensor) {
        sensor.AddObservation(food.localPosition);
        foreach(var segment in segments) {
            sensor.AddObservation(segment.localPosition);
        }
        sensor.AddObservation(this.transform.)

    }

    void OnActionReceived(ActionBuffers actionBuffers) {

    }


    /* SmoothLerp code repurposed from https://answers.unity.com/questions/1501234/smooth-forward-movement-with-a-coroutine.html */
    private IEnumerator SmoothLerp (float time, Transform obj, Vector3 startingPos, Vector3 finalPos) {
        float elapsedTime = 0;

        while (elapsedTime < time) {
            obj.position = Vector3.Lerp(startingPos, finalPos, (elapsedTime/time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.position = finalPos;
    }

    private void Grow() {
        Transform segment = Instantiate(segmentPrefab);
        segment.GetComponent<BoxCollider>().enabled = false;
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    private void ResetState() {
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        segments.Clear();
        segments.Add(transform);
        sizeCounter = initialSize;

        this.transform.position = Vector3.zero;
        targetTile = Vector3.zero;
    }

    private void OnTriggerEnter(Collider obj) {
        if (obj.tag == "Food") {
            grow = true;
        }
        else if (obj.tag == "Obstacle") {
            reset = true;
            print("collided");
        }
    }

}
