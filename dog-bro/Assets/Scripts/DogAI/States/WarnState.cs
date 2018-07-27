using UnityEngine;
using StateNameSpace;

interface IWarnState
{
    void IndicateDanger();
    bool dangerApparent { get; }
}

// Requires you to implement the following interfaces on the DogAI:
// - IWaitForSafetyState
public class WarnState : State<BasicAI>
{
    private static WarnState _instance;

    private WarnState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static WarnState Instance
    {
        get
        {
            if (_instance == null)
            {
                new WarnState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
        Debug.Log("State - Enter Warn");
        IWarnState ownerWithWarningInterface = owner as IWarnState;
        if (ownerWithWarningInterface != null) {
            ownerWithWarningInterface.IndicateDanger();
        }
    }

    public override void ExitState(BasicAI owner)
    {
        Debug.Log("State - Exit Warn");
    }

    // Can change to States:
    // -> WaitForSafetyState
    public override void UpdateState(BasicAI owner)
    {
        owner.stateMachine.ChangeState(WaitForSafetyState.Instance);
    }
}
