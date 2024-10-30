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

    public override void OnTriggerEnter(UnitStateManager _unit, Collider other)
    {
        if (other.gameObject.CompareTag(_unit.myEnemyTag))
        {
            _unit.enemiesInRange++;
            _unit.SwitchStates(_unit.fightState);
        }
    }

    public override void OnTriggerExit(UnitStateManager _unit, Collider other)
    {
        if (other.gameObject.CompareTag(_unit.myEnemyTag))
        {
            _unit.enemiesInRange--;
            if (_unit.enemiesInRange <= 0)
            {
                _unit.enemiesInRange = 0;
                _unit.SwitchStates(_unit.idleState);
            }
        }
    }

}
