using UnityEditor;
using UnityEngine;

public class UnitFightState : UnitBaseState
{
    public override void EnterState(UnitStateManager _unit)
    {
        _unit.StopMoving();
        //_unit.mAnimator.Play("Shoot");
        Fight(_unit);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        return;
    }

    public override void OnTriggerExit(UnitStateManager _unit, Collider other)
    {
        if (other.gameObject.CompareTag(_unit.myEnemyTag))
        {
            _unit.enemiesInRange--;
            if (_unit.enemiesInRange <= 0 )
            {
                _unit.enemiesInRange = 0;
                _unit.SwitchStates(_unit.idleState);
            }
        }
    }

    public void Fight(UnitStateManager _unit)
    {
        if (_unit.enemiesInRange != 0)
        {
            _unit.life -= 10 * _unit.enemiesInRange;
            _unit.WaitTimer(1.5f);
            
        }
    }

}
