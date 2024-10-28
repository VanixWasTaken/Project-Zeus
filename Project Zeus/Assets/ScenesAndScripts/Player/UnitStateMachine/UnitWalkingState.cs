using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AudioType = UnityEngine.Audio.AudioType;

public class UnitWalkingState : UnitBaseState
{

    private float speed = 5f;
    private float rotationSpeed = 15f;

    public override void EnterState(UnitStateManager _unit)
    {
        _unit.mAnimator.SetFloat("anSpeed", 1);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        _unit.transform.position = Vector3.MoveTowards(_unit.transform.position, _unit.targetPosition, speed * Time.deltaTime);

        if (_unit.transform.position == _unit.targetPosition)
        {
            _unit.SwitchStates(_unit.idleState);
        }

        Vector3 direction = _unit.targetPosition - _unit.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _unit.transform.rotation = Quaternion.Slerp(_unit.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public override void OnFootstep(UnitStateManager _unit)
    {
        _unit.audioController.RandomizeAudioPitch(AudioType.SMFootstep_01, 0.8f, 1.2f);
    }
}
