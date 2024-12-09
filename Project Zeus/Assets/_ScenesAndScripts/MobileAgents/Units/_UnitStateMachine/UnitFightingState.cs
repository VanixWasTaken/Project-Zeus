using UnityEngine;

public class UnitFightingState : UnitBaseState
{



    #region Unity Built-In

    public override void EnterState(UnitStateManager _unit)
    {
        _unit.navMeshAgent.ResetPath();

        _unit.animator.SetFloat("anSpeed", 0);
        _unit.animator.SetTrigger("anShouldShoot");
        _unit.animator.SetBool("anIsShooting", true);

        LookAtEnemy(_unit);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        if (_unit.enemyStateManager.health == 0)
        {
            _unit.SwitchStates(_unit.idleState);
        }
    }

    #endregion


    #region Custom Functions()

    private void LookAtEnemy(UnitStateManager _unit)
    {
        _unit.navMeshAgent.updateRotation = false;
        _unit.transform.LookAt(_unit.nearestEnemyPosition);
    }

    #endregion

}
