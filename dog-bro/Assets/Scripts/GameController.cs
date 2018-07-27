using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public TextController textController;
    public BlindController blindController;

    public GameObject player;
    public GameObject dog;

    bool gameFinished = false;

    private void Start()
    {
        Collider playerCollider = player.GetComponent<Collider>();
        Collider dogCollider = dog.GetComponent<Collider>();

        Physics.IgnoreCollision(playerCollider, dogCollider);
        //Physics.IgnoreLayerCollision(dog.layer, player.layer, true);
    }

    public void GoalReached()
    {

        if (!gameFinished)
        {
            textController.ShowGoalReachedText();

            if (blindController.IsBlind())
            {
                blindController.ToggleBlindness();
            }
            gameFinished = true;
        }

    }

    public bool IsGameFinished()
    {
        return gameFinished;
    }

}
