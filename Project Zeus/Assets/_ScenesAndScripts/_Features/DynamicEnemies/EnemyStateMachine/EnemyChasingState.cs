using Unity.Cinemachine;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    bool unitReached = false;

    public override void EnterState(EnemyStateManager _enemy)
    {
        Debug.Log("Chasing!");
        _enemy.ActivateLight();
        _enemy.animator.SetBool("anIsChasing", true);
        _enemy.animator.SetFloat("anSpeed", 1f);
        _enemy.animator.SetTrigger("anShouldChase");
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
            _enemy.animator.SetBool("anIsChasing", false);
            _enemy.animator.SetFloat("anSpeed", 0);
            _enemy.SwitchState(_enemy.roamingState);
        }
    }

    private void ChaseUnit(EnemyStateManager _enemy)
    {
        if (_enemy.GetTarget() != null)
        {
            Vector3 targetPosition = _enemy.GetTarget().transform.position;
            _enemy.MoveToTarget(targetPosition);
        }
        else
        {
            _enemy.SwitchState(_enemy.roamingState);
        }
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
            _enemy.navMeshAgent.stoppingDistance = 2;
            if (_enemy.navMeshAgent.remainingDistance < _enemy.navMeshAgent.stoppingDistance)
            {
                unitReached = true;
                Debug.Log("I have reached the target!");
            }
            else
            {
                UpdateUnitPosition(_enemy);
            }

            if (unitReached)
            {
                // Switch to attack state once the enemy is close enough to the player
                Debug.Log("I am switching to the attack state");
                _enemy.animator.SetFloat("anSpeed", 0);
                _enemy.animator.SetBool("anIsChasing", false);
                _enemy.SwitchState(_enemy.attackingState);
            }
        }
    }
}
