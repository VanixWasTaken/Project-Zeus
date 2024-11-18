using UnityEngine;

public class UnitIdleState : UnitBaseState
{
    public override void EnterState(UnitStateManager _unit)
    {
        _unit.animator.SetFloat("anSpeed", 0);
    }

    public override void UpdateState(UnitStateManager _unit)
    {

    }
}
