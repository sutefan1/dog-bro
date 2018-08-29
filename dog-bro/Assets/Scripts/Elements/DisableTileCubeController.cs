using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTileCubeController : MonoBehaviour {
    private GameController gameController;
    public Transform head;
    // Use this for initialization
    void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(head.transform.position.x, 0, head.transform.position.z);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            gameController.AddTactilePavingToDisable(other.gameObject.transform.parent.gameObject);
        }
    }
}
