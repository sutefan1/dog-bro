using UnityEngine;
using StateNameSpace;

interface IDangerZoneState
{
    void IndicateImmediateDanger();
    void IndicateImmediateDangerIsGone();
    bool immediateDangerApparent { get; }
}

// Requires you to implement the following interfaces on the DogAI:
// - IDangerZoneState
public class EnterDangerZoneState : State<BasicAI>
{
    private static EnterDangerZoneState _instance;

    private EnterDangerZoneState()
    {
        if (_instance != null)
        {
            return;
        }
        _instance = this;
    }

    public static EnterDangerZoneState Instance
    {
        get
        {
            if (_instance == null)
            {
                new EnterDangerZoneState();
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
    // -> WaitForSafteyInDangerZoneState
    public override void UpdateState(BasicAI owner)
    {
        owner.stateMachine.ChangeState(WaitForSafteyInDangerZoneState.Instance);
    }
}
