using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public GameObject cameraRig;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        OnTriggerStay(other);
    }

    private void OnTriggerStay(Collider other)
    {
        TrafficController trafficController = other.gameObject.GetComponent<TrafficController>();
        if (trafficController != null && trafficController.isCurrentlyRed)
        {
            //TODO: call the reduce health or reset to last save point function!!!
            Vector3 playerPosition = cameraRig.transform.position;
            cameraRig.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z - 2);
        }
    }
}
