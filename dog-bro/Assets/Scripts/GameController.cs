using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public TextController textController;
    public BlindController blindController;
    public Transform cameraRigTransform;

    private List<GameObject> disabledTiles  = new List<GameObject>();

    public GameObject player;
    public GameObject dog;

    public bool levelFinished = false;
    private bool gameFinished = false;

    private LevelParser levelParser;
    private int level = 1;
    private PlayerController playerController;

    private AudioSource audioTutorialPlayer;
    bool movementAudioHasBeenPlayed = false;

    private Vector3 _resetPosition;
    public Vector3 resetPosition
    {
        get
        {
            return _resetPosition;
        }
        set
        {
            _resetPosition = value;
            disabledTiles.Clear();
        }
    }

    private bool _isIntroPlaying = false;
    public bool isIntroPlaying
    {
        get
        {
            return _isIntroPlaying;
        }
        set
        {
            _isIntroPlaying = value;
        }
    }

    private void Awake()
    {
        audioTutorialPlayer = gameObject.AddComponent<AudioSource>();
        audioTutorialPlayer.volume = 0.1f; // Because otherwise you wouldn't hear the walking
    }

    private void Start()
    {
        levelParser = gameObject.GetComponent<LevelParser>();
        LoadNextLevel();
        
        Collider playerCollider = player.GetComponent<Collider>();
        Collider dogCollider = dog.GetComponent<Collider>();
        
        Physics.IgnoreCollision(playerCollider, dogCollider);
        playerController = player.GetComponent<PlayerController>();
    }

    public void GoalReached()
    {
        cameraRigTransform.transform.position = new Vector3(100, 100, 100);
        level += 1;
        levelFinished = true;

        if (level == 11) {
            gameFinished = true;
            PlayOutroAudio();
        }
    }

    public void RightTriggerClicked()
    {
        if (levelFinished == true && !gameFinished)
        {
            LoadNextLevel();
            levelFinished = false;
        }
        else
        {
           // blindController.ToggleBlindness();
        }
    }

    private string getLevelString(int levelNumber)
    {
        return "Assets/Levels/dog-bro_" + levelNumber + ".json";
    }


    public void OnPlayerDeath()
    {
        //On Death 
        //go to the last saved position
        PlayAudio("Audio/ResetAudio");
        ResetPlayer();
    }
    public void AddTactilePavingToDisable(GameObject parentTile)
    {
        parentTile.SetActive(false);
        disabledTiles.Add(parentTile);
    }

    private void LoadNextLevel() {

        levelParser.LoadLevel(getLevelString(level));
        ResetPlayer();


        TryPlayingNextIntroAudio();
    }

    void Update()
    {
        if (audioTutorialPlayer != null && audioTutorialPlayer.isPlaying)
        {
            isIntroPlaying = true;
        }
        else
        {
            isIntroPlaying = false;
        }

        TryPlayingMovementAudio();
    }


    public void ResetPlayer() {
        foreach(GameObject tile in disabledTiles)
        {
            tile.SetActive(true);
        }
        disabledTiles.Clear();
        cameraRigTransform.transform.position = _resetPosition;
        dog.gameObject.transform.position = _resetPosition;

    }
    public int GetLevel()
    {
        return level;
    }


    // MARK: Audio Stuff

    void TryPlayingNextIntroAudio()
    {
        if (level == 1)
        {
            PlayAudio("Audio/track01Intro");
        }
        else if (level == 4)
        {
            PlayAudio("Audio/track03IntroDog");
        }
        else if (level == 5)
        {
            PlayAudio("Audio/track04TutorialEndGameStart");
        }
    }

    void TryPlayingMovementAudio()
    {

        if (!movementAudioHasBeenPlayed && level == 1 && isIntroPlaying == false && playerController.isMoving)
        {
            movementAudioHasBeenPlayed = true;
            PlayAudio("Audio/track02Intro");
        }
    }

    void PlayOutroAudio() 
    {
        PlayAudio("Audio/track05GameEndHomeReached");
    }

    public void PlayCliffResetAudio() 
    {
        PlayAudio("Audio/ResetAudio");
    }

    public void PlayTrafficResetAudio() 
    {
        // TODO!!!!
        PlayAudio("Audio/TrafficResetAudio");
    }

    void PlayAudio(string fileName)
    {
        if (fileName != null)
        {

            if(audioTutorialPlayer.isPlaying) {
                audioTutorialPlayer.Stop();
            }

            isIntroPlaying = true;
            audioTutorialPlayer.PlayOneShot((AudioClip)Resources.Load(fileName));
        }
    }


}
