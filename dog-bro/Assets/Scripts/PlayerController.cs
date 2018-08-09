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
        if (other.gameObject.name == "TrafficCollider" || cliffController != null)
        {
            gameController.ResetPlayer();
        }
    }
}
