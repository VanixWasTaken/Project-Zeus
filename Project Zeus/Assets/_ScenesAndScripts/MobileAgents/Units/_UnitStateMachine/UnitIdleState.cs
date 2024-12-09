using UnityEngine;

public class UnitIdleState : UnitBaseState
{
    #region Unity Built-In

    public override void EnterState(UnitStateManager _unit)
    {
        ResetAnimation(_unit);

        _unit.navMeshAgent.updateRotation = true;
    }

    public override void UpdateState(UnitStateManager _unit)
    {

    }

    #endregion


    #region Custom Functions()

    private void ResetAnimation(UnitStateManager _unit)
    {
        _unit.animator.SetFloat("anSpeed", 0); // plays the idle animation
        _unit.animator.SetBool("anIsShooting", false);
    }

    #endregion
}
