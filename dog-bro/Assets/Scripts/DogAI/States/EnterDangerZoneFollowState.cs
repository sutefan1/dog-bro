using UnityEngine;
using StateNameSpace;

public class EnterDangerZoneFollowState : State<BasicAI>
{
    private static EnterDangerZoneFollowState _instance;

    private EnterDangerZoneFollowState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static EnterDangerZoneFollowState Instance
    {
        get
        {
            if (_instance == null)
            {
                new EnterDangerZoneFollowState();
            }
            return _instance;
        }
    }

    public override void EnterState(BasicAI owner)
    {
        IDangerZoneState ownerWithDangerZoneInterface = owner as IDangerZoneState;
        if (ownerWithDangerZoneInterface != null)
        {
            ownerWithDangerZoneInterface.IndicateImmediateDanger();
        }
    }

    public override void ExitState(BasicAI owner)
    {
    }

    // Can change to States:
    // -> WaitForSafteyInDangerZoneFollowState
    public override void UpdateState(BasicAI owner)
    {
        owner.stateMachine.ChangeState(WaitForSafteyInDangerZoneFollowState.Instance);
    }
}
