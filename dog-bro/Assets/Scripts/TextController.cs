using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    public GameObject goalReachedText;

    private bool tutoralIsRunning = false;

    // Currently, only show a basic text that explains what the goal is and how to control the game
    // Potentially replace it with a voice explenation instead of text.
    public void ToggleTutorial()
    {
        //tutoralIsRunning = !tutoralIsRunning;
        //tutorialText.enabled = tutoralIsRunning;
    }

    public void ShowGoalReachedText()
    {
        goalReachedText.active = true;
    }
}
