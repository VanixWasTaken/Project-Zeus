using UnityEngine;

public class WorkerWalkingState : WorkerBaseState
{


    public override void EnterState(WorkerStateManager _worker)
    {
        _worker.animator.SetFloat("anSpeed", 1);
    }

    public override void UpdateState(WorkerStateManager _worker)
    {
        IsAtDestinationCheck(_worker);
    }



    #region Custom Functions()
    void IsAtDestinationCheck(WorkerStateManager _worker) // Check if the unit has reached its destination
    {
        
        if (_worker.navMeshAgent != null && !_worker.navMeshAgent.pathPending)
        {
            _worker.navMeshAgent.stoppingDistance = 3;
            if (_worker.navMeshAgent.remainingDistance <= _worker.navMeshAgent.stoppingDistance)
            {
                // Stop moving and switch to idle state if the destination is reached
                _worker.StopMoving();
            }
        }
    }
    #endregion
}
