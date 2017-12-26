using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Vector3 mousePos, forceDirection;
    public Vector3 mouseDirection;
    float distance;
    int rocketsInScene;
    public float force, rocketSpeed;
    public LayerMask playerMask;
    GameObject rocketLauncher, throwable;
    public GameObject[] rockets;

    void Start () {
        rocketLauncher = GameObject.FindGameObjectWithTag("rocketLauncher");
        rockets = new GameObject[30];
	}
	
	void Update () {
        transform.rotation = new Quaternion(0, 0, 0, 0);
        mousePos = Input.mousePosition;
        mousePos.z = 10;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mouseDirection = mousePos - transform.position;
        Ray ray = new Ray(rocketLauncher.transform.position, mousePos);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, playerMask);
        if (Input.GetMouseButtonDown(0))
        {
            if (rockets[0] != null && Vector3.Distance(rockets[rocketsInScene - 1].transform.position, transform.position) > 10) {
                rockets[rocketsInScene] = Instantiate(Resources.Load("rocket")) as GameObject;
                rockets[rocketsInScene].transform.position = rocketLauncher.transform.position;
                rockets[rocketsInScene].GetComponent<Rigidbody>().velocity = mouseDirection.normalized * rocketSpeed;
                rockets[rocketsInScene].name = "rocket" + rocketsInScene;
                Physics.IgnoreCollision(rockets[rocketsInScene].GetComponent<Collider>(), GetComponent<Collider>());
                rocketsInScene += 1;
            }
            if (rocketsInScene == 0) {
                rockets[0] = Instantiate(Resources.Load("rocket")) as GameObject;
                rockets[0].transform.position = rocketLauncher.transform.position;
                rockets[0].GetComponent<Rigidbody>().velocity = mouseDirection.normalized * rocketSpeed + GetComponent<Rigidbody>().velocity;
                rockets[0].name = "rocket" + rocketsInScene;
                Physics.IgnoreCollision(rockets[0].GetComponent<Collider>(), GetComponent<Collider>());
                rocketsInScene += 1;
            }
        }
        for (int i = 0; i < 20; i++) {
            if (rockets[i] != null && rockets[i].GetComponent<RocketScript>().collided)
            {
                Destroy(rockets[i]);
                rocketsInScene -= 1;
                forceDirection = rockets[i].GetComponent<RocketScript>().collidePosition - transform.position;
                distance = Vector3.Distance(rockets[i].GetComponent<RocketScript>().collidePosition, transform.position);
                if (distance < 10) {
                    GetComponent<Rigidbody>().velocity = -forceDirection.normalized * force;
                }
                for (int a = 0; a < 20; a++) {
                    rockets[System.Array.IndexOf(rockets, rockets[i]) + a] = rockets[System.Array.IndexOf(rockets, rockets[i]) + a + 1];
                }
            }
            if (rockets[i] != null && Vector3.Distance(rockets[i].transform.position, transform.position) > 100) {
                Destroy(rockets[i]);
                rocketsInScene -= 1;
                for (int a = 0; a < 20; a++) {
                    rockets[System.Array.IndexOf(rockets, rockets[i]) + a] = rockets[System.Array.IndexOf(rockets, rockets[i]) + a + 1];
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.G) && throwable == null) {
            throwable = Instantiate(Resources.Load("throwable")) as GameObject;
            Physics.IgnoreCollision(throwable.GetComponent<Collider>(), GetComponent<Collider>());
            throwable.transform.position = transform.position;
            throwable.GetComponent<Rigidbody>().velocity = mouseDirection.normalized * 6;
        }
    }
}
