using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficController : MonoBehaviour
{
    //public BoxCollider trafficCollider;
    public float trafficLightOnRed = 5f;
    public float trafficLightOnGreen = 10f;
    public bool startWithRedLight = true;
    public GameObject ground;

    private float counter;
    private bool _isCurrentlyRed;
    public bool isCurrentlyRed
    {
        get
        {
            return this._isCurrentlyRed;
        }
        set
        {
            //trafficCollider.enabled = value;
            ground.GetComponent<Renderer>().material = value ? crosswalkRedMaterial : crosswalkGreenMaterial;
            this._isCurrentlyRed = value;
        }
    }

    public Material crosswalkGreenMaterial;
    public Material crosswalkRedMaterial;

	// Use this for initialization
	void Start () {
        counter = startWithRedLight ? trafficLightOnRed : trafficLightOnGreen;
        isCurrentlyRed = startWithRedLight;
	}
	
	// Update is called once per frame
	void Update () {
        counter -= Time.deltaTime;
        if(counter <= 0) {
            counter = isCurrentlyRed ? trafficLightOnGreen : trafficLightOnRed;
            isCurrentlyRed = !isCurrentlyRed;
        }
	}
}
