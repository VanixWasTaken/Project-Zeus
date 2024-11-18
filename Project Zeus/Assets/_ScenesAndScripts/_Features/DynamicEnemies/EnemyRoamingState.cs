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
        Debug.Log("Roaming!");
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        if (detectedObjects.Count > 0)
        {
            GameObject nearestObject = DetermineNearestObject(_enemy, detectedObjects);

            if (nearestObject != null)
            {
                _enemy.SetTarget(nearestObject);
                _enemy.SwitchState(_enemy.chasingState);
            }
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
        GameObject targetObject = null;

        for (int i = 0; i < _objects.Count; i++)
        {
            distance = Vector3.Distance(_enemy.transform.position, _objects[i].transform.position);

            if (distance < nearestDistance)
            {
                targetObject = _objects[i];
                nearestDistance = distance;
            }
        }

        if (targetObject != null)
        {
            return targetObject;
        }
        else
        {
            Debug.LogError("Error: No target object was found! Returning null!");
            return null;
        }
    }
}
