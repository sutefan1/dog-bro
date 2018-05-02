using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTileController : TactileController {

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Notify that the goal has been reached
        // TODO: Add a rigidbody to moving object. Otherwise, this will never be triggered

        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if (playerController != null)
        {
            Debug.Log("Goal Reached!");

            gameController.GoalReached();
        }
    }
}
