using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour {

    GameObject player;
    Vector3 mouseDirection;

	void Start () {
        player = GameObject.FindGameObjectWithTag("player");
	}
	
	void Update () {
        mouseDirection = player.GetComponent<PlayerMovement>().mouseDirection;
        Quaternion rotation = Quaternion.LookRotation(mouseDirection, this.transform.up);
        transform.rotation = rotation;
        transform.position = player.transform.position + mouseDirection.normalized*0.7f;
	}

}
