using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public TextController textController;
    public BlindController blindController;

    private GameObject CameraPlayer;
    private Vector3 startPosition;

    bool gameFinished = false;

    void Start()
    {
        CameraPlayer = GameObject.Find("[CameraRig]");

        //Position of Starting tile
        startPosition = Vector3.zero;
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


    /* Player Positions - checkpoints and death
     */
    public void UpdatePlayerStartPosition(Vector3 newPosition)
    {
        //If a checkpoint is passed, 
        //the "startPosition" variable of "GameController" is set to this position 
        startPosition = newPosition;
    }

    public void ResetPlayerToLastCheckPoint()
    {
        //Go To Last CheckPoint Position
        CameraPlayer.transform.position = this.GetPlayerStartPosition();
    }

    public Vector3 GetPlayerStartPosition()
    {
        return startPosition;
    }
    public void OnPlayerDeath()
    {
        //On Death 
        //go to the last saved position
        CameraPlayer.transform.position = this.GetPlayerStartPosition();
    }
}
