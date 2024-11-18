using Unity.Cinemachine;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    bool workerReached = false;

    public override void EnterState(EnemyStateManager _enemy)
    {
        Debug.Log("Chasing!");
        ChaseUnit(_enemy);
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        // Check if the enemy has reached the units
        if (_enemy.navMeshAgent != null && !_enemy.navMeshAgent.pathPending)
        {
            _enemy.navMeshAgent.stoppingDistance = 3;
            if (_enemy.navMeshAgent.remainingDistance < _enemy.navMeshAgent.stoppingDistance)
            {
                workerReached = true;
            }
            else
            {
                UpdateUnitPosition(_enemy);
            }

            if (workerReached)
            {
                // Switch to attack state once the enemy is close enough to the player
                _enemy.SwitchState(_enemy.attackingState);
            }
        }
    }

    private void ChaseUnit(EnemyStateManager _enemy)
    {
        Vector3 targetPosition = _enemy.GetTarget().transform.position;
        _enemy.MoveToTarget(targetPosition);
    }

    private void UpdateUnitPosition(EnemyStateManager _enemy)
    {
        _enemy.navMeshAgent.destination = _enemy.GetTarget().transform.position;
    }
}
