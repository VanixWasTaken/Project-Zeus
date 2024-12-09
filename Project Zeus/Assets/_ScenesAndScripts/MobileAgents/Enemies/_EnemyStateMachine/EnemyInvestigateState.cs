using UnityEngine;

public class EnemyInvestigateState : EnemyBaseState
{



    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        ResetAnimations(_enemy);

    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        
    }

    #endregion



    #region Custom Functions()

    private void ResetAnimations(EnemyStateManager _enemy)
    {
        _enemy.animator.SetBool("anIsChasing", false);
        _enemy.animator.SetBool("anIsAttacking", false);
        _enemy.animator.SetTrigger("anShouldWalk");
        _enemy.animator.SetBool("anIsWalking", true);

    }

    #endregion
}
