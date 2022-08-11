using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private Vector3 direction;
    private Vector3 targetTile;
    private Vector3 currTile;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        direction = Vector3.right;
        targetTile = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") < 0) {
            direction = Vector3.right;
        }
        else if (Input.GetAxis("Horizontal") > 0) {
            direction = Vector3.left;
        }
        else if (Input.GetAxis("Vertical") < 0) {
            direction = Vector3.down;
        }
        else if (Input.GetAxis("Vertical") > 0) {
            direction = Vector3.up;
        }
    }

    void FixedUpdate() {
        currTile = new Vector3 (
            Mathf.Round(transform.position.x),
            Mathf.Round(transform.position.y),
            Mathf.Round(transform.position.z)
        );
        if (targetTile.Equals(transform.position)) {
            targetTile = currTile + direction;
            StartCoroutine(SmoothLerp(1/speed));
        }
    }

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
