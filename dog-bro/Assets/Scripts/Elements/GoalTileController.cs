using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTileController : TactileController {

    private void OnTriggerEnter(Collider other)
    {
        // TODO: Notify that the goal has been reached
        Debug.Log("Wuhu - Goal Reached!");
    }
}
