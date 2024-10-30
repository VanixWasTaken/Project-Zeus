using UnityEditor;
using UnityEngine;

public class UnitFightState : UnitBaseState
{
    GameObject currentEnemy;
    public override void EnterState(UnitStateManager _unit)
    {
        _unit.StopMoving();
        _unit.mAnimator.SetTrigger("shouldAttack");
        _unit.mAnimator.SetBool("isAttacking", true);
        _unit.mAnimator.SetFloat("anSpeed", 1);
        Fight(_unit);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        return;
    }


    public void Fight(UnitStateManager _unit)
    {
        if (_unit.enemiesInRange.Count > 0)
        {
            currentEnemy = _unit.enemiesInRange[0];
            _unit.transform.LookAt(currentEnemy.transform.position);
            Vector3 rotation = _unit.transform.rotation.eulerAngles;
            rotation.y += 60;
            _unit.transform.rotation = Quaternion.Euler(rotation);
            if (currentEnemy == null)
            {
                _unit.enemiesInRange.RemoveAt(0);
                return;
            }
            currentEnemy.GetComponent<UnitStateManager>().TakeDamage(_unit.damage);
            if (currentEnemy.GetComponent<UnitStateManager>().life <= 0)
            {
                currentEnemy = null;
                _unit.enemiesInRange.RemoveAt(0);
            }
            _unit.WaitTimer(1.5f);
        }
        else
        {
            _unit.mAnimator.SetBool("isAttacking", false);
            _unit.SwitchStates(_unit.idleState);
        }
    }

}
