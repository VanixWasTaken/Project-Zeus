using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
{

    #region Variables

    Vector3 _defaultPatrolPostion1;
    Vector3 _defaultPatrolPostion2;
    Vector3 _lastPatrolDestination;

    #endregion



    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        ResetAnimations(_enemy);

        InitializePatrolRoute(_enemy); // If there isn't set a custom patrol route, it will add a default route.

        _enemy.navMeshAgent.speed = 2;
        _enemy.navMeshAgent.SetDestination(_enemy.patrolPoints[0]);
        _lastPatrolDestination = _enemy.patrolPoints[0];
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        Patrol(_enemy);

        ChaseUnit(_enemy);
    }

    #endregion


    #region Custom Functions()

    private void InitializePatrolRoute(EnemyStateManager _enemy) // Sets up the patrol route based on user-defined or default positions
    {
        if (_enemy.patrolPoints.Count == 0)
        {
            _defaultPatrolPostion1 = _enemy.transform.position += new Vector3(0, 0, 8);
            _defaultPatrolPostion2 = _enemy.transform.position += new Vector3(0, 0, -8);

            _enemy.patrolPoints.Add(_defaultPatrolPostion1);
            _enemy.patrolPoints.Add(_defaultPatrolPostion2);
        }
    }

    private void Patrol(EnemyStateManager _enemy) // Makes the enemy patrol between custom defined or if those wherent set, the default points
    {
        if (_enemy.navMeshAgent.remainingDistance <= 0)
        {
            int currendIndex = _enemy.patrolPoints.IndexOf(_lastPatrolDestination);
            int nextIndex = (currendIndex + 1) % _enemy.patrolPoints.Count; // Cycle through the patrol points
        
            _enemy.navMeshAgent.SetDestination(_enemy.patrolPoints[nextIndex]);
            _lastPatrolDestination = _enemy.patrolPoints[nextIndex];
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
