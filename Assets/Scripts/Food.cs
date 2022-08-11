using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    public BoxCollider spawnArea;

    // Start is called before the first frame update
    void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition() {

        Bounds bound = spawnArea.bounds;

        float x = Random.Range(bound.min.x, bound.max.x);
        float y = Random.Range(bound.min.y, bound.max.y);
        float z = Random.Range(bound.min.z, bound.max.z);

        transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), Mathf.Round(z));

    }

    void OnTriggerEnter(Collider box) {
        if (box.tag == "Player") {
            RandomizePosition();
        }
    }

}
