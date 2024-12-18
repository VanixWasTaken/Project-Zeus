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
        
        ResetAnimations(_enemy);

        _enemy.navMeshAgent.ResetPath();
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        ReturnToRoaming(_enemy);
    }

    #endregion


    #region Custom Functions()

    private void ResetAnimations(EnemyStateManager _enemy)
    {
        _enemy.animator.SetBool("anIsAttacking", true);
        _enemy.animator.SetTrigger("anShouldAttack");
        _enemy.animator.SetBool("anIsWalking", false);
        _enemy.animator.SetBool("anIsChasing", false);
        _enemy.animator.ResetTrigger("anShouldScream"); // Currently bugging so I reset the trigger per hand
    }

    private void ReturnToRoaming(EnemyStateManager _enemy)
    {
        if (_enemy.unitStateManager == null)
        {
            _enemy.unitSpotted = false;
            _enemy.SwitchState(_enemy.roamingState);
        }
    }

    #endregion

}
