using UnityEngine;

public class UnitMiningState : UnitBaseState
{
    public override void EnterState(UnitStateManager _unit)
    {
        _unit.mAnimator.SetTrigger("anShouldMine");
        _unit.mAnimator.SetBool("anIsMining", true);
        _unit.StopMoving();
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        return;
    }
}
