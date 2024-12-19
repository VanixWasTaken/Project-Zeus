using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EnemyAttackingState : EnemyBaseState
{
  

    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        _enemy.ActivateLight();

        _enemy.ChangeAnimationState(EnemyStateManager.ENEMY_ATTACKING);

        _enemy.navMeshAgent.ResetPath();
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        ReturnToRoaming(_enemy);
    }

    #endregion


    #region Custom Functions()

    private void ReturnToRoaming(EnemyStateManager _enemy)
    {
        if (!_enemy.shouldAttackUnits)
        {
            _enemy.SwitchState(_enemy.roamingState);
        }
    }

    #endregion

}
