using UnityEngine;

public class UnitWalkingState : UnitBaseState
{
    #region Unity Built-In

    public override void EnterState(UnitStateManager _unit)
    {
        // starts walking animation
        _unit.animator.SetBool("anIsMining", false);
        _unit.animator.SetFloat("anSpeed", 1);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        IsAtDestinationCheck(_unit); // checks if the unit reached its goal
    }

    #endregion


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