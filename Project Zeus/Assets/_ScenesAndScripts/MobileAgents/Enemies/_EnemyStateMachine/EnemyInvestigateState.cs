using UnityEngine;

public class EnemyInvestigateState : EnemyBaseState
{



    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        _enemy.ChangeAnimationState(EnemyStateManager.ENEMY_WALKING);
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        
    }

    #endregion

}
