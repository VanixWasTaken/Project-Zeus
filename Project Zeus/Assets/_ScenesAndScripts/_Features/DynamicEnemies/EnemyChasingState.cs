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
        NavigationCheck(_enemy);
    }

    public override void OnTriggerExit(EnemyStateManager _enemy, Collider _collision)
    {
        if (_collision.gameObject == _enemy.GetTarget())
        {
            _enemy.SetTarget(null);
            _enemy.SwitchState(_enemy.roamingState);
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

    private  void NavigationCheck(EnemyStateManager _enemy)
    {
        // Check if the enemy has reached the units
        if (_enemy.navMeshAgent != null && !_enemy.navMeshAgent.pathPending)
        {
            _enemy.navMeshAgent.stoppingDistance = 3;
            if (_enemy.navMeshAgent.remainingDistance < _enemy.navMeshAgent.stoppingDistance)
            {
                workerReached = true;
                Debug.Log("I have reached the target!");
            }
            else
            {
                Debug.Log("I am updating the target position");
                UpdateUnitPosition(_enemy);
            }

            if (workerReached)
            {
                // Switch to attack state once the enemy is close enough to the player
                Debug.Log("I am switching to the attack state");
                _enemy.SwitchState(_enemy.attackingState);
            }
        }
    }
}
