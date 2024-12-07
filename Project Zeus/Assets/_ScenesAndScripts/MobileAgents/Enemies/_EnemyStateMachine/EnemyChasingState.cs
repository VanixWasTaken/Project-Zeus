using Unity.Cinemachine;
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

        _enemy.navMeshAgent.speed = 3.5f;
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        ChaseUnit(_enemy);
    }



    #endregion


    #region Custom Functions()

    private void LightUpAndStartAnims(EnemyStateManager _enemy) 
    {
        _enemy.ActivateLight();
        _enemy.animator.SetBool("anIsChasing", true);
        _enemy.animator.SetFloat("anSpeed", 1f);
        _enemy.animator.SetTrigger("anShouldChase");
    }

    private void ChaseUnit(EnemyStateManager _enemy)
    {
        _enemy.navMeshAgent.SetDestination(_enemy.lastKnownUnitPosititon);

        GoBackToRoaming(_enemy); // If there is no unit on sight anymore, gets back to his old patrol positions
    }

    private void GoBackToRoaming(EnemyStateManager _enemy)
    {
        if (_enemy.navMeshAgent.remainingDistance <= 0 && !_enemy.unitSpotted)
        {
            _enemy.SwitchState(_enemy.roamingState);
        }
    }

    #endregion

}
