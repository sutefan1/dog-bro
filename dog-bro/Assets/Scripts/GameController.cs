using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public TextController textController;
    public BlindController blindController;
    public Transform cameraRigTransform;

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
        }
    }

    private void Start()
    {
        levelParser = gameObject.GetComponent<LevelParser>();
        LoadNextLevel();
    }

    public void GoalReached()
    {
        cameraRigTransform.transform.position = new Vector3(100,100,100);
        level += 1;
        LoadNextLevel();
    }

    private string getLevelString(int levelNumber)
    {
        return "Assets/Levels/dog-bro_" + levelNumber + ".json";
    }

    private void LoadNextLevel() {
        
        levelParser.LoadLevel(getLevelString(level));
        ResetPlayer();
    }

    public void ResetPlayer() {
        cameraRigTransform.transform.position = _resetPosition;
    }
}
