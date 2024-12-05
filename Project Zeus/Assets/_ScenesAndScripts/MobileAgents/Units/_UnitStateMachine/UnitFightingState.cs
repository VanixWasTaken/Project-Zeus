using UnityEngine;

public class UnitFightingState : UnitBaseState
{



    #region Unity Built-In

    public override void EnterState(UnitStateManager _unit)
    {
        _unit.navMeshAgent.SetDestination(_unit.transform.position);

        _unit.animator.SetFloat("anSpeed", 0);
        _unit.animator.SetTrigger("anShouldShoot");
        _unit.animator.SetBool("anIsShooting", true);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        if (_unit.enemy.health == 0)
        {
            _unit.SwitchStates(_unit.idleState);
        }
    }

    #endregion


}
