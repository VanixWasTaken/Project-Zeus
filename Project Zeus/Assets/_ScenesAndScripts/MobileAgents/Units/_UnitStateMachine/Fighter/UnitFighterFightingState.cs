using System.Collections.Generic;
using UnityEngine;

public class UnitFighterFightingState : UnitBaseState
{

    #region References

    private GameObject targetObject;

    #endregion

    #region Variables

    private float distance;
    private float nearestDistance = 100f;
   
    #endregion


    #region Unity Build In

    public override void EnterState(UnitStateManager _unit)
    {
        SetAttackAnimations(_unit);

        ResetAttackAnimations(_unit);


        _unit.mainTarget = DetermineNearestObject(_unit, _unit.enemiesInRange);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        
    }

    #endregion



    #region Custom Functions()

    public override void OnEnemyHit(UnitStateManager _unit)
    {

        if (_unit.mainTarget != null)
        {
            _unit.transform.LookAt(_unit.mainTarget.transform);

            EnemyStateManager stateManager = _unit.mainTarget.GetComponent<EnemyStateManager>();

            if (stateManager.life > 0)
            {
                stateManager.TakeDamage(_unit, 3);
            }
            else if (stateManager.life <= 0)
            {
                _unit.mainTarget = null;
                _unit.enemiesInRange.Remove(stateManager.gameObject);
                stateManager.Die();

                if (_unit.enemiesInRange.Count > 0)
                {
                    _unit.mainTarget = DetermineNearestObject(_unit, _unit.enemiesInRange);
                }
                else
                {
                    _unit.animator.SetBool("isAttacking", false);
                    _unit.animator.SetFloat("anSpeed", 0);
                    _unit.SwitchStates(_unit.idleState);
                }
            }
        }
    }

    private GameObject DetermineNearestObject(UnitStateManager _unit, List<GameObject> _objects)
    {
        // iterate through all objects in list and determine nearest object
        for (int i = 0; i < _objects.Count; i++)
        {
            distance = Vector3.Distance(_unit.transform.position, _objects[i].transform.position);

            if (distance < nearestDistance)
            {
                targetObject = _objects[i];
                nearestDistance = distance;
            }
        }

        // return the nearest object (targetObject)
        if (targetObject != null)
        {
            return targetObject;
        }

        // if anything went wrong, log an error to console
        else
        {
            Debug.LogError("Error: No target object was found! Returning null!");
            return null;
        }
    }

    private void SetAttackAnimations(UnitStateManager _unit)
    {
        _unit.animator.SetBool("isAttacking", true);
        _unit.animator.SetTrigger("shouldAttack");
    }

    private void ResetAttackAnimations(UnitStateManager _unit)
    {
        if (DetermineNearestObject(_unit, _unit.enemiesInRange) == null)
        {
            _unit.animator.SetBool("isAttacking", false);
            _unit.animator.SetFloat("anSpeed", 0);
            _unit.SwitchStates(_unit.idleState);
        }
    }

    #endregion






}
