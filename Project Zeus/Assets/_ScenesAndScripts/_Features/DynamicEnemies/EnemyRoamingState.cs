using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
{
    private List<GameObject> detectedObjects = new List<GameObject>();
    float distance;
    float nearestDistance = 100;

    public override void EnterState(EnemyStateManager _enemy)
    {
        for (int i = 0; i < detectedObjects.Count; i++) 
        {
            Debug.Log(detectedObjects[i]);
        }
        Debug.Log("Roaming!");
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        // If the object detected 1 or more objects, check what object is nearest and switch to attack state
        if (detectedObjects.Count > 0)
        {
            GameObject nearestObject = DetermineNearestObject(_enemy, detectedObjects);

            ExitAndUpdateList(_enemy, nearestObject);
        }
    }

    private IEnumerator RoamingBehaviour(EnemyStateManager _enemy)
    {
        // implement behaviour for roaming around their designated area
        yield break;
    }

    public override void OnTriggerEnter(EnemyStateManager _enemy, Collider _collision)
    {
        detectedObjects.Add(_collision.gameObject);
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
            if (detectedObjects.Contains(_nearestObject))
            {
                detectedObjects.Remove(_nearestObject);
            }
            _enemy.SwitchState(_enemy.chasingState);
        }
    }
}
