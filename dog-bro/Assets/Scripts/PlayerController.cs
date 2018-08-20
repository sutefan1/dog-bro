using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private GameController gameController;
    

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
        
    private void OnTriggerEnter(Collider other)
    {
        CliffController cliffController = other.gameObject.GetComponent<CliffController>();
        TrafficController trafficController = other.gameObject.GetComponent<TrafficController>();
        if (cliffController != null || trafficController != null)
        {
            gameController.ResetPlayer();
        }
        
    }
}
