using UnityEngine;
using StateNameSpace;

public class IndicateSafteyFollowState : State<BasicAI>
{
    private static IndicateSafteyFollowState _instance;

    private IndicateSafteyFollowState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static IndicateSafteyFollowState Instance
    {
        get
        {
            if (_instance == null)
            {
                new IndicateSafteyFollowState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
        IIndicateSafteyState ownerWithIndicateSafetyInterface = owner as IIndicateSafteyState;

        if(ownerWithIndicateSafetyInterface != null) {
            ownerWithIndicateSafetyInterface.IndicateSafety();
        }

    }

    public override void ExitState(BasicAI owner)
    {
    }

    // Can change to States:
    // -> FollowState
    public override void UpdateState(BasicAI owner)
    {
        owner.stateMachine.ChangeState(FollowState.Instance);
    }
}
