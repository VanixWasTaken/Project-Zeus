using UnityEngine;

public class UnitWalkingState : UnitBaseState
{
    #region Unity Built-In

    public override void EnterState(UnitStateManager _unit)
    {
        ResetAnimations(_unit); // Resets the animations and plays the walk animation

        _unit.navMeshAgent.updateRotation = true;
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        IsAtDestinationCheck(_unit); // checks if the unit reached its goal
    }

    #endregion


    #region Custom Functions()

    private void ResetAnimations(UnitStateManager _unit)
    {
        #region Sound Reset und Start
        _unit.sound.StopSoundByType(UnitSoundHelper.SoundType.SHOOTING);

        if (_unit.GetClass() == UnitStateManager.UnitClass.Fighter)
        {
            _unit.sound.PlaySoundByType(UnitSoundHelper.SoundType.MOVING);
        }

        #endregion

        _unit.animator.SetBool("anIsMining", false);
        _unit.animator.SetBool("anIsShooting", false);
        _unit.animator.SetFloat("anSpeed", 1);
    }

    private void IsAtDestinationCheck(UnitStateManager _unit) // Check if the unit has reached its destination
    {

        if (_unit.navMeshAgent != null && !_unit.navMeshAgent.pathPending)
        {
            _unit.navMeshAgent.stoppingDistance = 3;
            if (_unit.navMeshAgent.remainingDistance <= _unit.navMeshAgent.stoppingDistance)
            {
                // Stop moving and switch to idle state if the destination is reached
                _unit.StopMoving();
                _unit.sound.StopSoundByType(UnitSoundHelper.SoundType.MOVING);
            }
        }
    }

    #endregion
}
