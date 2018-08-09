using StateNameSpace;

// Requires you to implement the following interfaces on the DogAI:
// - IDangerZoneState
public class ExitDangerZoneState : State<BasicAI>
{
    private static ExitDangerZoneState _instance;

    private ExitDangerZoneState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static ExitDangerZoneState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ExitDangerZoneState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
        IDangerZoneState ownerWithDangerZoneInterface = owner as IDangerZoneState;
        if (ownerWithDangerZoneInterface != null)
        {
            ownerWithDangerZoneInterface.IndicateImmediateDangerIsGone();
        }
    }

    public override void ExitState(BasicAI owner)
    {
    }

    // Can change to States:
    // -> WaitForSafteyInDangerZoneState
    public override void UpdateState(BasicAI owner)
    {
        owner.stateMachine.ChangeState(WaitForSafetyState.Instance);
    }
}
