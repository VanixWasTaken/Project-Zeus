using UnityEngine;

public class UnitWalkingState : UnitBaseState
{


    public override void EnterState(UnitStateManager _unit)
    {
        _unit.animator.SetFloat("anSpeed", 1);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        IsAtDestinationCheck(_unit);
    }



    #region Custom Functions()
    void IsAtDestinationCheck(UnitStateManager _unit) // Check if the unit has reached its destination
    {

        if (_unit.navMeshAgent != null && !_unit.navMeshAgent.pathPending)
        {
            _unit.navMeshAgent.stoppingDistance = 3;
            if (_unit.navMeshAgent.remainingDistance <= _unit.navMeshAgent.stoppingDistance)
            {
                // Stop moving and switch to idle state if the destination is reached
                _unit.StopMoving();
            }
        }
    }
    #endregion
}
