using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour {

    public bool collided = false;
    public Vector3 collidePosition;
    public int rocketNumber = 0;

    void OnCollisionEnter(Collision other) {
        if (!collided) {
            collidePosition = transform.position;
            collided = true;
        }
    }
}
