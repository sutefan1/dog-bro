using UnityEngine;
using StateNameSpace;

interface IWaitForSafetyState
{
    bool safteyTrigger { get; }
}

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
        Debug.Log("State - Enter WaitForSafteyState");
    }

    public override void ExitState(BasicAI owner)
    {
        Debug.Log("State - Exit WaitForSafteyState");
    }

    // Can change to States:
    // -> IndicateSafteyState, if safteyTrigger is true
    // -> WaitForSafetyState, else
    public override void UpdateState(BasicAI owner)
    {
        IWaitForSafetyState ownerWithWaitForSafetyInterface = owner as IWaitForSafetyState;
        IFollowState ownerWithFollowInterface = owner as IFollowState;

        if(ownerWithWaitForSafetyInterface != null && ownerWithWaitForSafetyInterface.safteyTrigger == true)
        {
            owner.stateMachine.ChangeState(IndicateSafteyState.Instance);
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
