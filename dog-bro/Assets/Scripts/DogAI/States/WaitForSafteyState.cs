using UnityEngine;
using StateNameSpace;

// Requires you to implement the following interfaces on the DogAI:
// - IWaitForSaftey
// - IFollowState
public class WaitForSafetyState : State<BasicAI>
{
    private static WaitForSafetyState _instance;

    private WaitForSafetyState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static WaitForSafetyState Instance
    {
        get
        {
            if (_instance == null)
            {
                new WaitForSafetyState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
    }

    public override void ExitState(BasicAI owner)
    {
    }

    // Can change to States:
    // -> IndicateSafteyState, if safteyTrigger is true
    // -> WaitForSafetyState, else
    public override void UpdateState(BasicAI owner)
    {
        IWarnState ownerWithWarnInterdace = owner as IWarnState;
        IFollowState ownerWithFollowInterface = owner as IFollowState;
        IDangerZoneState ownerWithDangerZoneInterface = owner as IDangerZoneState;

        if (ownerWithWarnInterdace != null && ownerWithWarnInterdace.dangerApparent == false)
        {
            owner.stateMachine.ChangeState(IndicateSafteyState.Instance);
        }
        else if (ownerWithDangerZoneInterface != null && ownerWithDangerZoneInterface.immediateDangerApparent == true) 
        {
            owner.stateMachine.ChangeState(EnterDangerZoneState.Instance);
        }
        else if(ownerWithFollowInterface != null && ownerWithFollowInterface.shouldFollow == true) 
        {
            owner.stateMachine.ChangeState(WaitForSafetyFollowState.Instance);
        } 
        else 
        {
            owner.stateMachine.ChangeState(WaitForSafetyState.Instance);
        }
    }
}
