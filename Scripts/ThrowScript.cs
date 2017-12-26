using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowScript : MonoBehaviour {



	void Start () {
		
	}
	
	void Update () {
        Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("player").GetComponent<Collider>(), GetComponent<Collider>());
    }
    void OnCollisionEnter() {
        Destroy(gameObject);
    }
}
