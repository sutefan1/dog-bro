using UnityEngine;
using StateNameSpace;

// Requires you to implement the following interfaces on the DogAI:
// - IWaitForSaftey
// - IFollowState
public class WaitForSafetyFollowState : State<BasicAI>
{
    private static WaitForSafetyFollowState _instance;

    private WaitForSafetyFollowState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static WaitForSafetyFollowState Instance
    {
        get
        {
            if (_instance == null)
            {
                new WaitForSafetyFollowState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
        IFollowState ownerWithFollowInterface = owner as IFollowState;

        if (ownerWithFollowInterface != null)
        {
            ownerWithFollowInterface.Move();
        }
    }

    public override void ExitState(BasicAI owner)
    {
    }

    // Can change to States:
    // -> IndicateSafteyFollowState, if safteyTrigger is true
    // -> WaitForSafetyFollowState, else
    public override void UpdateState(BasicAI owner)
    {
        IWarnState ownerWitWarnInterface = owner as IWarnState;
        IFollowState ownerWithFollowInterface = owner as IFollowState;
        IDangerZoneState ownerWithDangerZoneInterface = owner as IDangerZoneState;

        if (ownerWitWarnInterface != null && ownerWitWarnInterface.dangerApparent == false)
        {
            owner.stateMachine.ChangeState(IndicateSafteyFollowState.Instance);
        }
        else if (ownerWithDangerZoneInterface != null && ownerWithDangerZoneInterface.immediateDangerApparent == true)
        {
            owner.stateMachine.ChangeState(EnterDangerZoneFollowState.Instance);
        }
        else if (ownerWithFollowInterface != null && ownerWithFollowInterface.shouldFollow == false) 
        {
            owner.stateMachine.ChangeState(WaitForSafetyState.Instance);
        } 
        else
        {
            owner.stateMachine.ChangeState(WaitForSafetyFollowState.Instance);
        }
    }
}
