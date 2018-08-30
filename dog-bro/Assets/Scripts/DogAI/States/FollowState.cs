using UnityEngine;
using StateNameSpace;

public interface IFollowState
{
    void Move();
    bool shouldFollow { get; }
}

// Requires you to implement the following interfaces on the DogAI:
// - IFollowState
// - IWarnState

public class FollowState : State<BasicAI>
{
    private static FollowState _instance;

    private FollowState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static FollowState Instance {
        get
        {
            if (_instance == null)
            {
                new FollowState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
        IFollowState ownerWithFollowInterface = owner as IFollowState;

        if(ownerWithFollowInterface != null) 
        {
            ownerWithFollowInterface.Move();
        }
    }

    public override void ExitState(BasicAI owner)
    {
    }

    // Can change to States:
    // -> IdleState, if shouldFollow is false
    // -> WarnFollowState, if dangerApparent is true
    // -> FollowState, else
    public override void UpdateState(BasicAI owner)
    {
        IFollowState ownerWithFollowInterface = owner as IFollowState;
        IWarnState ownerWithWarningInterface = owner as IWarnState;

        if (ownerWithWarningInterface != null && ownerWithWarningInterface.dangerApparent == true)
        {
            owner.stateMachine.ChangeState(WarnFollowState.Instance);
        }
        else if (ownerWithFollowInterface != null && ownerWithFollowInterface.shouldFollow == false)
        {
            owner.stateMachine.ChangeState(IdleState.Instance);
        }
        else if(ownerWithFollowInterface != null) 
        {
            owner.stateMachine.ChangeState(FollowState.Instance);
        }

    }
}
