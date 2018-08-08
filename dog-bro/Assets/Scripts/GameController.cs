using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public TextController textController;
    public BlindController blindController;

    private GameObject CameraPlayer;
    private Vector3 PlayerLastCheckPointPosition;

    bool gameFinished = false;

    void Start()
    {
        CameraPlayer = GameObject.Find("[CameraRig]");
        PlayerLastCheckPointPosition = Vector3.zero;
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


    /* Player Position - checkpoints and death
     */
    public void UpdatePlayerLastPosition(Vector3 newPosition)
    {
        Debug.Log("GameController UpdatePlayerLastPosition old " + CameraPlayer.transform.position);
        Debug.Log("GameController UpdatePlayerLastPosition new " + newPosition);
        Debug.Log("GameController UpdatePlayerLastPosition PlayerLastCheckPointPosition " + PlayerLastCheckPointPosition);
        //If a checkpoint is passed, 
        //the "startPosition" variable of "GameController" is set to this position 
        CameraPlayer.transform.position = newPosition;
        PlayerLastCheckPointPosition = newPosition;
        Debug.Log("GameController UpdatePlayerLastPosition new " + CameraPlayer.transform.position);
        Debug.Log("GameController UpdatePlayerLastPosition PlayerLastCheckPointPosition " + PlayerLastCheckPointPosition);
    }

    public Vector3 GetPlayerLastPosition()
    {
        //return last position
        return PlayerLastCheckPointPosition;
    }
    public void OnPlayerDeath()
    {
        //On Death 
        //go to the last saved position
        Debug.Log("On Player Death this.GetPlayerLastPosition() " + this.GetPlayerLastPosition());
        Debug.Log("On Player Death CameraPosition " + CameraPlayer.transform.position);
        CameraPlayer.transform.position = this.GetPlayerLastPosition();
    }
}
