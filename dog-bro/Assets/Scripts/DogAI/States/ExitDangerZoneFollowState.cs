using StateNameSpace;

// Requires you to implement the following interfaces on the DogAI:
// - IDangerZoneState
public class ExitDangerZoneFollowState : State<BasicAI>
{
    private static ExitDangerZoneFollowState _instance;

    private ExitDangerZoneFollowState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static ExitDangerZoneFollowState Instance
    {
        get
        {
            if (_instance == null)
            {
                new ExitDangerZoneFollowState();
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
        owner.stateMachine.ChangeState(WaitForSafetyFollowState.Instance);
    }
}
