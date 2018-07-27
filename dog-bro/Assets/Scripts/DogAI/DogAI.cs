using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StateNameSpace;

public class BasicAI: MonoBehaviour {
    public StateMachine<BasicAI> stateMachine { get; set; }
}

public class DogAI : BasicAI, IFollowState, IWarnState, IWaitForSafetyState, IIndicateSafteyState {

    public Transform characterTransform;
    public Transform dogTransform;
    public float movementSpeed;

    public bool shouldFollow 
    {
        get 
        {
            if (Vector3.Distance(characterTransform.position, dogTransform.position) < 0.05)
            {
                return false;
            }
            else
            {
                return true;
            }
        }    
    }

    private bool _warnTrigger = false;
    public bool warnTrigger
    {
        get
        {
            return _warnTrigger;
        }
    }

    private bool _safteyTrigger = false;
    public bool safteyTrigger
    {
        get
        {
            return _safteyTrigger;
        }
    }

    private void Start()
    {
        // Create Statemachine and set inital state
        stateMachine = new StateMachine<BasicAI>(this, FollowState.Instance);
    }

    void Update()
    {
        stateMachine.Update();
    }



    // Implementation of IFollowState
    public void Move()
    {
        float step = movementSpeed * Time.deltaTime;
        dogTransform.position = Vector3.MoveTowards(dogTransform.position, characterTransform.position, step);
    }

    // Implementation of IWarnState
    public void IndicateDanger()
    {
        throw new System.NotImplementedException();
    }

    // Implementation of IIndicateSafteyState
    public void IndicateSafety()
    {
        throw new System.NotImplementedException();
    }
}
