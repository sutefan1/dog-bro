using StateNameSpace;

public class WaitForSafteyInDangerZoneFollowState : State<BasicAI>
{
    private static WaitForSafteyInDangerZoneFollowState _instance;

    private WaitForSafteyInDangerZoneFollowState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static WaitForSafteyInDangerZoneFollowState Instance
    {
        get
        {
            if (_instance == null)
            {
                new WaitForSafteyInDangerZoneFollowState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
        IFollowState ownerWithFollowInterface = owner as IFollowState;

        if (ownerWithFollowInterface != null)
        {
            ownerWithFollowInterface.Move();
        }
    }

    public override void ExitState(BasicAI owner)
    {
    }

    // Can change to States:
    // -> WaitForSafteyInDangerZoneState
    public override void UpdateState(BasicAI owner)
    {
        IFollowState ownerWithFollowInterface = owner as IFollowState;
        IDangerZoneState ownerWithDangerZoneInterface = owner as IDangerZoneState;

        if (ownerWithDangerZoneInterface != null && ownerWithDangerZoneInterface.immediateDangerApparent == false)
        {
            owner.stateMachine.ChangeState(ExitDangerZoneFollowState.Instance);
        }
        else if (ownerWithFollowInterface != null && ownerWithFollowInterface.shouldFollow == false)
        {
            owner.stateMachine.ChangeState(WaitForSafteyInDangerZoneState.Instance);
        }
        else
        {
            owner.stateMachine.ChangeState(WaitForSafteyInDangerZoneFollowState.Instance);
        }
    }
}