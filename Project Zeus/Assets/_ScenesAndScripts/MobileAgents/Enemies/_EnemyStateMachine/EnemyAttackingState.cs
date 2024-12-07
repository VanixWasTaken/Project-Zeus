using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EnemyAttackingState : EnemyBaseState
{
    #region References

    private UnitStateManager target;

    #endregion

    #region Variables

    private float distance;
    private float nearestDistance = 100f;

    #endregion



    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        _enemy.ActivateLight();
        _enemy.animator.SetBool("anIsAttacking", true);
        _enemy.animator.SetTrigger("anShouldAttack");
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        // no need for implementation
    }

    

    #endregion


    #region Custom Functions()

  

    #endregion

}
