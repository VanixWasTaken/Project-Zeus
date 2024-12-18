

using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    #region Variables

    private bool unitReached = false;

    #endregion



    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        LightUpAndStartAnims(_enemy); // Start Animations and ActivateLight

        _enemy.navMeshAgent.speed = _enemy.chasingSpeed;
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        ChaseUnit(_enemy); // Switch to ChaseState if unit was spotted

        AttackUnits(_enemy); // Switch to AttackState if unit is in enemy range
    }

    #endregion




    #region Custom Functions()

    private void LightUpAndStartAnims(EnemyStateManager _enemy) 
    {
        _enemy.ActivateLight();
        _enemy.animator.SetBool("anIsChasing", true);
        _enemy.animator.SetFloat("anSpeed", 1f);
        _enemy.animator.SetTrigger("anShouldScream");
        _enemy.animator.SetBool("anIsWalking", false);
        _enemy.animator.ResetTrigger("anShouldWalk"); // Currently bugging so I reset the trigger per hand
    }

    private void ChaseUnit(EnemyStateManager _enemy)
    {
        if (_enemy.finishedScream)
        {
            _enemy.navMeshAgent.SetDestination(_enemy.lastKnownUnitPosititon);

            GoBackToRoaming(_enemy); // If there is no unit on sight anymore, gets back to his old patrol positions
        }
        else
        {
            _enemy.navMeshAgent.ResetPath();
        }
    }

    private void GoBackToRoaming(EnemyStateManager _enemy)
    {
        if (_enemy.navMeshAgent.remainingDistance <= 0 && !_enemy.unitSpotted)
        {
            _enemy.SwitchState(_enemy.roamingState);
        }
    }

    private void AttackUnits(EnemyStateManager _enemy)
    {
        if (_enemy.shouldAttackUnits)
        {
            _enemy.SwitchState(_enemy.attackingState);
        }
    }

    #endregion

}
