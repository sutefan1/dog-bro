using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class WhiteStickController : MonoBehaviour {

    public SteamVR_TrackedController controller;
    public SteamVR_LaserPointer laserPointer;
    public TextController textController;

    private void OnEnable()
    {
        laserPointer.PointerIn -= HandlePointerIn;
        laserPointer.PointerIn += HandlePointerIn;

        controller.TriggerClicked -= HandleTriggerClicked;
        controller.TriggerClicked += HandleTriggerClicked;
    }

    private void HandlePointerIn(object sender, PointerEventArgs e)
    {
  

        if (e.distance <= 1.6)
        {

            CliffTactileRailController cliffTactileRailController = e.target.GetComponent<CliffTactileRailController>();
            TactileRailController tactileRailController = e.target.GetComponent<TactileRailController>();

            if (cliffTactileRailController != null)
            {
                SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse(3900);
                //TODO: Cliff sound to get separation to other rails (haptic feedback)

            }

            if (tactileRailController != null)
            {
                SteamVR_Controller.Input((int)controller.controllerIndex).TriggerHapticPulse(3900);
            }
            
        }
    }

    void HandleTriggerClicked(object sender, ClickedEventArgs e)
    {
        textController.ToggleTutorial();
    }

}
