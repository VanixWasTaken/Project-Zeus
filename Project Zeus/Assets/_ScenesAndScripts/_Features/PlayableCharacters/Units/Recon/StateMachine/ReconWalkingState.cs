using UnityEngine;

public class ReconWalkingState : ReconBaseState
{


    public override void EnterState(ReconStateManager _recon)
    {
        _recon.animator.SetFloat("anSpeed", 1);
    }

    public override void UpdateState(ReconStateManager _recon)
    {
        IsAtDestinationCheck(_recon);
    }



    #region Custom Functions()
    void IsAtDestinationCheck(ReconStateManager _recon) // Check if the unit has reached its destination
    {

        if (_recon.navMeshAgent != null && !_recon.navMeshAgent.pathPending)
        {
            _recon.navMeshAgent.stoppingDistance = 3;
            if (_recon.navMeshAgent.remainingDistance <= _recon.navMeshAgent.stoppingDistance)
            {
                // Stop moving and switch to idle state if the destination is reached
                _recon.StopMoving();
            }
        }
    }
    #endregion
}
