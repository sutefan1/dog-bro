using UnityEngine;
using StateNameSpace;

interface IIndicateSafteyState
{
    void IndicateSafety();
}


public class IndicateSafteyState : State<BasicAI>
{
    private static IndicateSafteyState _instance;

    private IndicateSafteyState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static IndicateSafteyState Instance
    {
        get
        {
            if (_instance == null)
            {
                new IndicateSafteyState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
        IIndicateSafteyState ownerWithIndicateSafetyInterface = owner as IIndicateSafteyState;

        if (ownerWithIndicateSafetyInterface != null)
        {
            ownerWithIndicateSafetyInterface.IndicateSafety();
        }

    }

    public override void ExitState(BasicAI owner)
    {
    }

    // Can change to States:
    // -> IdleState
    public override void UpdateState(BasicAI owner)
    {
        owner.stateMachine.ChangeState(IdleState.Instance);
    }
}
