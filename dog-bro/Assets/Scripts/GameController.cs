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
}
