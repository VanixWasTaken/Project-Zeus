using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
{
    float distance;
    float nearestDistance = 100;


    public override void EnterState(EnemyStateManager _enemy)
    {
        _enemy.DeactivateLight();

        _enemy.animator.SetFloat("anSpeed", 1);

        if (_enemy.shouldPatrol)
        {
            _enemy.circleCenter = _enemy.centerPoint != null ? _enemy.centerPoint.position : _enemy.transform.position;

            // Generate patrol points around the circle
            _enemy.patrolPoints = GeneratePatrolPoints(_enemy.circleCenter, _enemy.radius, _enemy.segments);

            _enemy.StartCoroutine(_enemy.RoamingBehaviour());
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

    Vector3[] GeneratePatrolPoints(Vector3 _center, float _radius, int _segments)
    {
        Vector3[] points = new Vector3[_segments];
        float angleStep = 360f / _segments;

        for (int i = 0; i < _segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            points[i] = new Vector3(
                _center.x + Mathf.Cos(angle) * _radius,
                _center.y,
                _center.z + Mathf.Sin(angle) * _radius
            );
        }

        return points;
    }

    private GameObject DetermineNearestObject(EnemyStateManager _enemy, List<GameObject> _objects)
    {
        // set the object to null to be able to reference it
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

    private void ExitAndUpdateList(EnemyStateManager _enemy, GameObject _nearestObject)
    {
        if (_nearestObject != null)
        {
            _enemy.StopAllCoroutines();
            _enemy.navMeshAgent.isStopped = true;
            _enemy.navMeshAgent.ResetPath();
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
