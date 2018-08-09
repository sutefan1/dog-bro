using UnityEngine;
using StateNameSpace;

// Requires you to implement the following interfaces on the DogAI:
// - IFollowState
// - IWarnState

public class IdleState : State<BasicAI>
{
    private static IdleState _instance;

    private IdleState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static IdleState Instance
    {
        get
        {
            if (_instance == null)
            {
                new IdleState();
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
    // -> FollowState, if shouldFollow is true
    // -> WarnState, if warnTrigger is set
    // -> IdleState, else
    public override void UpdateState(BasicAI owner)
    {
        IFollowState ownerWithFollowInterface = owner as IFollowState;
        IWarnState ownerWithWarningInterface = owner as IWarnState;

        if (ownerWithFollowInterface != null && ownerWithFollowInterface.shouldFollow)
        {
            owner.stateMachine.ChangeState(FollowState.Instance);
        }
        else if (ownerWithWarningInterface != null && ownerWithWarningInterface.dangerApparent == true)
        {
            owner.stateMachine.ChangeState(WarnState.Instance);
        } else 
        {
            owner.stateMachine.ChangeState(IdleState.Instance);
        }
    }
}
