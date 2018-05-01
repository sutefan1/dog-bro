using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindController : MonoBehaviour {

    private float _fadeDuration = 1f;
    private bool isBlind;

    void Start()
    {
        
    }

    public void ToggleBlindness() 
    {
        isBlind = !isBlind;

        if(isBlind) {
            SteamVR_Fade.Start(Color.black, _fadeDuration, true);
        } else {
            SteamVR_Fade.Start(Color.clear, _fadeDuration, true);
        }
    }

    public bool IsBlind() {
        return isBlind;
    }

}
