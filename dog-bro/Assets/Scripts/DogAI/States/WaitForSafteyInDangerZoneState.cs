using StateNameSpace;

public class WaitForSafteyInDangerZoneState : State<BasicAI>
{
    private static WaitForSafteyInDangerZoneState _instance;

    private WaitForSafteyInDangerZoneState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static WaitForSafteyInDangerZoneState Instance
    {
        get
        {
            if (_instance == null)
            {
                new WaitForSafteyInDangerZoneState();
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
    // -> 
    public override void UpdateState(BasicAI owner)
    {
        IFollowState ownerWithFollowInterface = owner as IFollowState;
        IDangerZoneState ownerWithDangerZoneInterface = owner as IDangerZoneState;

        if (ownerWithDangerZoneInterface != null && ownerWithDangerZoneInterface.immediateDangerApparent == false)
        {
            owner.stateMachine.ChangeState(ExitDangerZoneState.Instance);
        }
        else if (ownerWithFollowInterface != null && ownerWithFollowInterface.shouldFollow == true)
        {
            owner.stateMachine.ChangeState(WaitForSafteyInDangerZoneFollowState.Instance);
        }
        else
        {
            owner.stateMachine.ChangeState(WaitForSafteyInDangerZoneState.Instance);
        }
    }
}