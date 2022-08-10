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
        targetTile = currTile + direction;
        transform.position = Vector3.Lerp(transform.position, targetTile, speed);
    }
}
