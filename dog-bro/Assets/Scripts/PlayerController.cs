using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private AudioSource walkingAudioSource;
    private GameController gameController;

    private bool _isMoving;
    public bool isMoving {
        get { return _isMoving;}
    }

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        walkingAudioSource = gameObject.GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CharacterIsMoving(bool isMoving) {

        if (isMoving && !walkingAudioSource.isPlaying) {
            walkingAudioSource.Play();
        } else if(!isMoving && walkingAudioSource.isPlaying)
        {
            walkingAudioSource.Stop();
        }

        _isMoving = isMoving;
    }


    private void OnTriggerEnter(Collider other)
    {
        CliffController cliffController = other.gameObject.GetComponent<CliffController>();
        TrafficController trafficController = other.gameObject.GetComponent<TrafficController>();
        if (cliffController != null || (trafficController != null && trafficController.isCurrentlyRed))
        {
            gameController.ResetPlayer();
        }

        if(cliffController != null) {
            gameController.PlayCliffResetAudio();
        } else if (trafficController != null) {
            gameController.PlayTrafficResetAudio();
        }
    }
}
