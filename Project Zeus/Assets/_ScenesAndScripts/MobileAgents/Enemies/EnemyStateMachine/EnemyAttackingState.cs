using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class EnemyAttackingState : EnemyBaseState
{
    #region References

    private UnitStateManager target;

    #endregion

    #region Variables

    private float distance;
    private float nearestDistance = 100f;

    #endregion



    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        _enemy.ActivateLight();
        _enemy.transform.LookAt(_enemy.GetTarget().transform);
        _enemy.animator.SetBool("anIsAttacking", true);
        _enemy.animator.SetTrigger("anShouldAttack");
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        // no need for implementation
    }

    public override void OnUnitHit(EnemyStateManager _enemy)
    {
        if (_enemy.GetTarget() != null)
        {
            AttackWorker(_enemy);
        }
        else
        {
            _enemy.SwitchState(_enemy.roamingState);
        }
    }

    #endregion


    #region Custom Functions()

    public void AttackWorker(EnemyStateManager _enemy)
    {
        /// <summary>
        /// Gets a reference to the nearest Unit / the target
        /// 
        /// Determines if the unit is alive or dead -> if alive: take damage
        /// --> if dead: determine wether to chase next target or switch to roaming
        /// </summary>
        
        GameObject _unit = _enemy.GetTarget();

        if (_unit != null)
        {
            target = _unit.GetComponent<UnitStateManager>();
        }
        else
        {
            _enemy.SetTarget(null);
            _enemy.SwitchState(_enemy.roamingState);
        }
        

        if (target != null)
        {
            if (target.health <= 0)
            {
                target.Die();
                _enemy.animator.SetBool("anIsAttacking", false);
                _enemy.SetTarget(null);
                _enemy.UpdateDetectedObjects(_unit);
                if (_enemy.GetDetectedObjects() != null)
                {
                    _enemy.SetTarget(DetermineNearestObject(_enemy, _enemy.GetDetectedObjects()));
                    _enemy.SwitchState(_enemy.chasingState);
                }
                else
                {
                    _enemy.SwitchState(_enemy.roamingState);
                }
            }
            else if (target.health > 0)
            {
                target.TakeDamage(25f, _enemy);
            } 
        }
        else 
        {
            _enemy.SetTarget(null);
            _enemy.animator.SetBool("anIsAttacking", false);
            _enemy.animator.SetFloat("anSpeed", 0);
            _enemy.UpdateDetectedObjects(_unit);
            _enemy.SwitchState(_enemy.roamingState);
        }
    }

    private GameObject DetermineNearestObject(EnemyStateManager _enemy, List<GameObject> _objects)
    {
        // set the object to the first object of the list
        GameObject targetObject = null;

        // iterate through all objects in list and determine nearest object
        for (int i = 0; i < _objects.Count; i++)
        {
            distance = Vector3.Distance(_enemy.transform.position, _objects[i].transform.position);

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

    #endregion

}
