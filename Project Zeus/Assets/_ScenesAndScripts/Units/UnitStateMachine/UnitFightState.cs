using UnityEngine;

public class UnitFightState : UnitBaseState
{
    public override void EnterState(UnitStateManager _unit)
    {
        Debug.Log("HALLO");
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        throw new System.NotImplementedException();
    }
}
