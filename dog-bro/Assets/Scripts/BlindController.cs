using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindController : MonoBehaviour {

    private GameController gameController;

    private float _fadeDuration = 1f;
    private bool isBlind;

    void Start()
    {
         gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    public void ToggleBlindness() 
    {

        if (gameController.IsGameFinished()) { return; }

        isBlind = !isBlind;

        if(isBlind) {
            SteamVR_Fade.Start(Color.black, _fadeDuration, true);
        } else {
            SteamVR_Fade.Start(Color.clear, _fadeDuration, true);
        }
    }


    public bool IsBlind() 
    {
        return isBlind;
    }

}
