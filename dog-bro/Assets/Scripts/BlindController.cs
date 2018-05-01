using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindController : MonoBehaviour {

    private float _fadeDuration = 2f;


    void Start()
    {
        SteamVR_Fade.Start(Color.black, 2, true);
    }

}
