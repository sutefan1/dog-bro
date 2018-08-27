using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public TextController textController;
    public BlindController blindController;
    public Transform cameraRigTransform;

    private GameObject CameraPlayer;

    private List<GameObject> disabledTiles  = new List<GameObject>();

    public GameObject player;
    public GameObject dog;

    bool levelFinished = false;

    private LevelParser levelParser;
    private int level = 1;

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
    AudioSource audioTutorial;

    private void Start()
    {
        levelParser = gameObject.GetComponent<LevelParser>();
        LoadNextLevel();
        
        Collider playerCollider = player.GetComponent<Collider>();
        Collider dogCollider = dog.GetComponent<Collider>();
        
        Physics.IgnoreCollision(playerCollider, dogCollider);
    }

    public void GoalReached()
    {
        cameraRigTransform.transform.position = new Vector3(100,100,100);
        level += 1;
        levelFinished = true;
    }

    public void RightTriggerClicked()
    {
        if (levelFinished == true)
        {
            LoadNextLevel();
            levelFinished = false;
        }
        else
        {
            blindController.ToggleBlindness();
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

        PlayTutorialAudios();
    }
    void PlayTutorialAudios()
    {
        if (level == 1)
        {
            isIntroPlaying = true;
            List<string> intoAudios1 = new List<string>();
            intoAudios1.Add("track01Intro");
            intoAudios1.Add("track02Intro");
            StartCoroutine(playIntroSound(intoAudios1));
        } else if (level == 4) {
            isIntroPlaying = true;
            List<string> intoAudios1 = new List<string>();
            intoAudios1.Add("track03IntroDog");
            StartCoroutine(playIntroSound(intoAudios1));
        }
    }
    IEnumerator playIntroSound(List<string> audios )
    {
        foreach (string s in audios) {

            audioTutorial = gameObject.AddComponent<AudioSource>();
            audioTutorial.PlayOneShot((AudioClip)Resources.Load(s ));
            //length of audio1 - wait for it
            if (audios.Count > 1)
            {
                yield return new WaitForSeconds(20);
            }
        }
    }
    void Update()
    {
        if (audioTutorial != null && audioTutorial.isPlaying)
        {
            isIntroPlaying = true;
        }
        else {
            isIntroPlaying = false;
        }
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
}
