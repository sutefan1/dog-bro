using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

    public Text tutorialText;

    private bool tutoralIsRunning = false;

    // Currently, only show a basic text that explains what the goal is and how to control the game
    // Potentially replace it with a voice explenation instead of text.
    public void ToggleTutorial()
    {
        tutoralIsRunning = !tutoralIsRunning;
        tutorialText.enabled = tutoralIsRunning;
    }
}
