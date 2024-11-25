using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
{
    float distance;
    float nearestDistance = 100;

    public override void EnterState(EnemyStateManager _enemy)
    {
        if (DetermineNearestObject(_enemy, _enemy.GetDetectedObjects()) == null)
        {
            RoamingBehaviour(_enemy);
        }

    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        // If the object detected 1 or more objects, check what object is nearest and switch to attack state
        if (_enemy.GetDetectedObjects().Count > 0)
        {
            GameObject nearestObject = DetermineNearestObject(_enemy, _enemy.GetDetectedObjects());

            ExitAndUpdateList(_enemy, nearestObject);
        }
    }

    private IEnumerator RoamingBehaviour(EnemyStateManager _enemy)
    {
        // implement behaviour for roaming around their designated area
        yield break;
    }


    private GameObject DetermineNearestObject(EnemyStateManager _enemy, List<GameObject> _objects)
    {
        // set the object to the first object of the list
        GameObject targetObject = _objects[0];

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

    private void ExitAndUpdateList(EnemyStateManager _enemy, GameObject _nearestObject)
    {
        if (_nearestObject != null)
        {
            _enemy.SetTarget(_nearestObject);
            if (_enemy.GetDetectedObjects().Contains(_nearestObject))
            {
                _enemy.GetDetectedObjects().Remove(_nearestObject);
            }

            if (_enemy.CompareTag("Screamer"))
            {
                _enemy.SwitchState(_enemy.screamerScreamingState);
            }
            else
            {
                _enemy.SwitchState(_enemy.chasingState);
            }
        }
    }
}
