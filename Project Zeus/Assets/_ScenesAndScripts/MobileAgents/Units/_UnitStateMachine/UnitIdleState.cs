using UnityEngine;

public class UnitIdleState : UnitBaseState
{
    #region Unity Built-In

    public override void EnterState(UnitStateManager _unit)
    {
        _unit.animator.SetFloat("anSpeed", 0); // plays the idle animation
    }

    public override void UpdateState(UnitStateManager _unit)
    {

    }

    #endregion

}
