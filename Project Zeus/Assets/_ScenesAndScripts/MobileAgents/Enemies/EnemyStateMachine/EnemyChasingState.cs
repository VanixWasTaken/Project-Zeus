using Unity.Cinemachine;
using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    #region Variables

    private bool unitReached = false;

    #endregion



    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        LightUpAndStartAnims(_enemy); // Start Animations and ActivateLight

        ChaseUnit(_enemy); // Start Chasing Behaviour
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        NavigationCheck(_enemy); // Check if enemy reached target
    }


    #region Colliders

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

    #endregion

    #endregion


    #region Custom Functions()

    private void LightUpAndStartAnims(EnemyStateManager _enemy) 
    {
        _enemy.ActivateLight();
        _enemy.animator.SetBool("anIsChasing", true);
        _enemy.animator.SetFloat("anSpeed", 1f);
        _enemy.animator.SetTrigger("anShouldChase");
    }

    private void ChaseUnit(EnemyStateManager _enemy)
    {
        /// <summary>
        /// If the enemy has a target: get its current position and move toward it
        /// 
        /// If there is no target: switch back to roaming state
        /// </summary>
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
        /// <summary>
        /// This functions checks if the enemy reached the target
        /// 
        /// If the target is not in range yet, update the targetposition of the
        /// navmesh-agent and keep chasing
        /// 
        /// if the target is in range, flip unitReached --> switch state to attacking
        /// 
        /// Before switching to attack state: disable animations and call SwitchState
        /// </summary>

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
                _enemy.animator.SetFloat("anSpeed", 0);
                _enemy.animator.SetBool("anIsChasing", false);
                _enemy.SwitchState(_enemy.attackingState);
            }
        }
    }

    #endregion

}
