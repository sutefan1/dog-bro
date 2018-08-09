using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public TextController textController;
    public BlindController blindController;

    bool gameFinished = false;

    private void Start()
    {
        LevelParser levelParser = gameObject.GetComponent<LevelParser>();
        levelParser.LoadLevel(getLevelString(1));
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

    private string getLevelString(int level)
    {
        return "Assets/Levels/dog-bro_" + level + ".json";
    }

}
