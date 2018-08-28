using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTileController : TactileController {

    public AudioSource objectAudio;
    private GameController gameController;

    AudioSource audioGameGoal;

    void Start()
    {
        objectAudio = GetComponent<AudioSource>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Notify that the goal has been reached
        // TODO: Add a rigidbody to moving object. Otherwise, this will never be triggered

        PlayerController playerController = other.gameObject.GetComponent<PlayerController>();

        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if (playerController != null)
        {
            Debug.Log("Goal Reached!");

            if (objectAudio != null) {
                if (gameController.GetLevel() <= 1)
                {
                    StartCoroutine(playIntroSound());
                }
                //Game End Audio
                else if (gameController.GetLevel() == 10)
                {
                    audioGameGoal = gameObject.AddComponent<AudioSource>();
                    audioGameGoal.PlayOneShot((AudioClip)Resources.Load("Audio/track05GameEndHomeReached"));
                }
                //Go To Next Level Press Key Audio
                else
                {
                    StartCoroutine(playLevelFinishedSound());
                }
            }

            gameController.GoalReached();
        }
    }


    IEnumerator playIntroSound()
    {
        audioGameGoal = gameObject.AddComponent<AudioSource>();
        audioGameGoal.PlayOneShot((AudioClip)Resources.Load("Audio/track04TutorialEndGameStart"));
        yield return new WaitForSeconds(12);
        //TODO: Uncomment this section if the sound is recorded and added to the resource folder!!!
        audioGameGoal = gameObject.AddComponent<AudioSource>();
        audioGameGoal.PlayOneShot((AudioClip)Resources.Load("Audio/PressTriggerToContinue"));
    }

    IEnumerator playLevelFinishedSound()
    {
        audioGameGoal = gameObject.AddComponent<AudioSource>();
        audioGameGoal.PlayOneShot((AudioClip)Resources.Load("Audio/GoalTileAudio"));
        yield return new WaitForSeconds(4);
        audioGameGoal = gameObject.AddComponent<AudioSource>();
        audioGameGoal.PlayOneShot((AudioClip)Resources.Load("Audio/PressTriggerToContinue"));
    }
}
