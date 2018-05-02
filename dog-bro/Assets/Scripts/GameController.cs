using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public TextController textController;
    public BlindController blindController;

    bool gameFinished = false;

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
