using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using AudioType = UnityEngine.Audio.AudioType;

public class UnitIdleState : UnitBaseState
{
    public override void EnterState(UnitStateManager _unit)
    {
        _unit.mAnimator.SetFloat("anSpeed", 0);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
       
    }

}
