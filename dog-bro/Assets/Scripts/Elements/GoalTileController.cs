using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTileController : TactileController {

    public AudioSource objectAudio;
    private GameController gameController;

    AudioSource audioGameGoal;

    private void Awake()
    {
        InitAudioGameGoal();
    }

    private void InitAudioGameGoal() {
        audioGameGoal = gameObject.AddComponent<AudioSource>();
        audioGameGoal.volume = 0.2f;
    }

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

        if (playerController != null && gameController.GetLevel() < 10)
        {
            Debug.Log("Goal Reached!");

            if (objectAudio != null) {
                StartCoroutine(playLevelFinishedSound());
            }

            gameController.GoalReached();
        }
    }

    IEnumerator playLevelFinishedSound()
    {
        if(audioGameGoal == null) {
            // Should not happen, but better save than sorry
            InitAudioGameGoal();
        }

        audioGameGoal.PlayOneShot((AudioClip)Resources.Load("Audio/GoalTileAudio"));
        yield return new WaitForSeconds(1.5f);
        audioGameGoal = gameObject.AddComponent<AudioSource>();
        audioGameGoal.PlayOneShot((AudioClip)Resources.Load("Audio/PressTriggerToContinue"));
    }
}
