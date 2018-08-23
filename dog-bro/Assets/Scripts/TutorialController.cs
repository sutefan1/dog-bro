using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    private GameController gameController;
    public AudioSource tutorialAudio;
    private int level;

    // Use this for initialization
    void Start()
    {
        if ((gameController == null) && (GetComponent<GameController>() != null))
        {
            gameController = GetComponent<GameController>();
        }

        tutorialAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if ((gameController != null))
        {
            this.level = gameController.getLevel();
        }
        // If the player passes through the checkpoint, we activate it
        if (other.gameObject.name == "Player")
        {
            //Play tutorial audio in tutorial level
            if (level <= 1)
            {
                tutorialAudio.Play();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {

        if ((gameController != null))
        {
            this.level = gameController.getLevel();
            //    this.level = gameController.getLevel();
        }
        // If the player passes through the checkpoint, we activate it
        if (other.gameObject.name == "Player")
        {
            //Play tutorial audio in tutorial level
            if (level <= 1)
            {
                tutorialAudio.Stop();
            }
        }
    }
}
