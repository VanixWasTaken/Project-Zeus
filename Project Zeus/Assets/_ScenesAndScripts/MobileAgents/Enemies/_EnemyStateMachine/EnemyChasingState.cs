

using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    #region Variables

    private bool unitReached = false;

    #endregion



    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        _enemy.ActivateLight(); // Activate a red light for visiblity of the enemy
        
        FMODUnity.RuntimeManager.PlayOneShot(_enemy.audioSheet.GetSFXByName(FMODAudioData.SoundID.SFXEnemyScreamerScream));

        _enemy.ChangeAnimationState(EnemyStateManager.ENEMY_SCREAM);

        _enemy.navMeshAgent.speed = _enemy.chasingSpeed;
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        ChaseUnit(_enemy); // Switch to ChaseState if unit was spotted

        AttackUnits(_enemy); // Switch to AttackState if unit is in enemy range
    }

    #endregion


    



    #region Custom Functions()

    private void ChaseUnit(EnemyStateManager _enemy)
    {
        if (_enemy.finishedScream)
        {
            _enemy.ChangeAnimationState(EnemyStateManager.ENEMY_CHASING);
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
        if (_enemy.navMeshAgent.remainingDistance <= 0 && !_enemy.unitSpotted && _enemy.currentState == this)
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
