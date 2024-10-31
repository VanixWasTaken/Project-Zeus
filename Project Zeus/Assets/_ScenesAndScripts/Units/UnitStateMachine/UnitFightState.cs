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
        if (_unit.enemiesInRange.Count > 0 && !_unit.isDead)
        {
            currentEnemy = _unit.enemiesInRange[0];
            if (currentEnemy == null)
            {
                _unit.enemiesInRange.RemoveAt(0);
                return;
            }
            else
            {
                _unit.transform.LookAt(currentEnemy.transform.position);
                Vector3 rotation = _unit.transform.rotation.eulerAngles;
                rotation.y += 60;
                _unit.transform.rotation = Quaternion.Euler(rotation);
                currentEnemy.GetComponent<UnitStateManager>().TakeDamage(_unit.damage);
                if (currentEnemy.GetComponent<UnitStateManager>().life <= 0)
                {
                    currentEnemy = null;
                    _unit.enemiesInRange.RemoveAt(0);
                    if (_unit.enemiesInRange.Count >= 1)
                    {
                        currentEnemy = _unit.enemiesInRange[0];
                    }
                    else if (_unit.enemiesInRange.Count <= 0)
                    {
                        _unit.SwitchStates(_unit.idleState);
                        _unit.mAnimator.SetBool("isFighting", false);
                    }

                }
                _unit.WaitTimer(1.5f);
            }

        }
        else
        {
            _unit.mAnimator.SetBool("isAttacking", false);
            _unit.SwitchStates(_unit.idleState);
        }
    }

}
