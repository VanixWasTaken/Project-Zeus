using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AudioType = UnityEngine.Audio.AudioType;

public class UnitWalkingState : UnitBaseState
{

    public override void EnterState(UnitStateManager _unit)
    {
        _unit.mAnimator.SetFloat("anSpeed", 1);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        // Check if the unit has reached its destination
        if (_unit.navMeshAgent != null && !_unit.navMeshAgent.pathPending)
        {
            if (_unit.navMeshAgent.remainingDistance <= _unit.navMeshAgent.stoppingDistance)
            {
                // Stop moving and switch to idle state if the destination is reached
                _unit.StopMoving();
            }
        }
    }

    public override void OnFootstep(UnitStateManager _unit)
    {
        _unit.audioController.RandomizeAudioPitch(AudioType.DroneFootstep_01, 0.8f, 1.2f);
    }
}
