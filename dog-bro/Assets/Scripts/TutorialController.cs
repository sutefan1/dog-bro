using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    private GameController gameController;
    public AudioSource tutorialAudio;

    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        tutorialAudio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if (other.gameObject.name == "Player")
        {
            if (gameController.GetLevel() <= 1)
            {
                tutorialAudio.Play();
            }
        }
    }
}
