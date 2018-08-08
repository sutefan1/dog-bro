using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPoint : MonoBehaviour {

    /// <summary>
    /// Indicate if the checkpoint is activated
    /// </summary>
    public bool Activated = false;

    private GameController gameController;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("DeathPoint other name " + other.name);
        // If the player passes through the checkpoint, we activate it
        if (other.name == "Player")
        {
            gameController.OnPlayerDeath();
        }
    }
}
