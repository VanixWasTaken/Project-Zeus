using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
{

    #region Variables

    Vector3 _patrolPostion1;
    Vector3 _patrolPostion2;
    Vector3 lastPatrolDestination;

    #endregion



    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        ResetAnimations(_enemy);

        SetDefaultPatrolRoute(_enemy); // If there isn't set a custom patrol route, it will add a default route.

        _enemy.navMeshAgent.speed = 2;
        _enemy.navMeshAgent.SetDestination(_patrolPostion1);
        lastPatrolDestination = _patrolPostion1;
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        StartPatrol(_enemy);

        ChaseUnit(_enemy);
    }

    #endregion


    #region Custom Functions()

    private void SetDefaultPatrolRoute(EnemyStateManager _enemy)
   {
        if (_patrolPostion1 == Vector3.zero)
        {
            _patrolPostion1 = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y, _enemy.transform.position.z + 4);
            _patrolPostion2 = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y, _enemy.transform.position.z - 4);
        }

        if (_enemy.patrolPoints.Count == 0)
        {
            _enemy.patrolPoints.Add(_patrolPostion1);
            _enemy.patrolPoints.Add(_patrolPostion2);
        }
    }

    private void StartPatrol(EnemyStateManager _enemy)
    {
        if (_enemy.navMeshAgent.remainingDistance <= 0)
        {
            if (lastPatrolDestination == _enemy.patrolPoints[0])
            {
                _enemy.navMeshAgent.SetDestination(_patrolPostion2);
                lastPatrolDestination = _patrolPostion2;
            }
            else if (lastPatrolDestination == _enemy.patrolPoints[1])
            {
                _enemy.navMeshAgent.SetDestination(_patrolPostion1);
                lastPatrolDestination = _patrolPostion1;
            }
        }
    }

    private void ChaseUnit(EnemyStateManager _enemy)
    {
        if (_enemy.unitSpotted)
        {
            _enemy.SwitchState(_enemy.chasingState);
        }
    }

    private void ResetAnimations(EnemyStateManager _enemy)
    {
        _enemy.animator.SetFloat("anSpeed", 1);
        _enemy.animator.SetBool("anIsAttacking", false);
        _enemy.animator.SetBool("anIsChasing", false);
        _enemy.animator.SetTrigger("anShouldWalk");
        _enemy.animator.SetBool("anIsWalking", true);
    }

    #endregion

}
