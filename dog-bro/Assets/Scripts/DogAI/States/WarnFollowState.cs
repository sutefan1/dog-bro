using UnityEngine;
using StateNameSpace;

// Requires you to implement the following interfaces on the DogAI:
// - IWaitForSafetyState
public class WarnFollowState : State<BasicAI>
{
    private static WarnFollowState _instance;

    private WarnFollowState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static WarnFollowState Instance
    {
        get
        {
            if (_instance == null)
            {
                new WarnFollowState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
        IWarnState ownerWithWarningInterface = owner as IWarnState;
        Debug.Log("State - Enter WarnFollow");

        if (ownerWithWarningInterface != null) {
            ownerWithWarningInterface.IndicateDanger();
        }
    }

    public override void ExitState(BasicAI owner)
    {
        Debug.Log("State - Exit Warnfollow");
    }

    // Can change to States:
    // -> FollowWaitForSafteySate
    public override void UpdateState(BasicAI owner)
    {
        owner.stateMachine.ChangeState(WaitForSafetyFollowState.Instance);
    }
}
