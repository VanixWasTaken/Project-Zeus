using UnityEngine;

public class EnemyRoamSoundState : EnemyBaseState
{

    #region Unity Build In

    public override void EnterState(EnemyStateManager _enemy)
    {
        _enemy.navMeshAgent.SetDestination(_enemy.lastHeardSoundPosition);
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
       if (_enemy.navMeshAgent.remainingDistance <= 0)
        {
            _enemy.SwitchState(_enemy.roamingState);
        }

        FollowSoundSource(_enemy);
    }

    #endregion


    #region Custom Functions()

    private void FollowSoundSource(EnemyStateManager _enemy)
    {
        if (_enemy.unitSpotted)
        {
            _enemy.SwitchState(_enemy.chasingState);
        }
    }


    #endregion
}
