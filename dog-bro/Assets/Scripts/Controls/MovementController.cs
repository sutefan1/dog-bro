using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public SteamVR_TrackedController controller;
    public BlindController blindController;

    private SteamVR_Controller.Device device;


    private void OnEnable()
    {
        controller.TriggerClicked -= HandleTriggerClicked;
        controller.TriggerClicked += HandleTriggerClicked;

        device = SteamVR_Controller.Input((int)controller.controllerIndex);

    }

    void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        blindController.ToggleBlindness();
    }
	
	// Update is called once per frame
	void Update () {

        if(blindController.IsBlind()) {

            float xAxis = device.GetAxis().x;
            float yAxis = device.GetAxis().y;

            // TODO: Make this into movement
        }

	}
}
