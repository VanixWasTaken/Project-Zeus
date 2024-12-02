using System.Collections.Generic;
using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
{
    #region Variables

    float distance;
    float nearestDistance = 100;

    #endregion



    #region Unity Built-In

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

    #endregion


    #region Custom Functions()

    private Vector3[] GeneratePatrolPoints(Vector3 _center, float _radius, int _segments)
    {
        /// <summary>
        /// 
        /// This method generates the points of the patrol circle.
        /// 
        /// Creates and Array with the size of the circle segments and calculates how many
        /// degrees are between each patrol point.
        /// 
        /// Then it calculates where each point is, based on the degrees and radius inside
        /// the for loop and then returns the points array.
        /// 
        /// </summary>
        
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
        /// <summary>
        /// This function takes a list of GameObjects and determines the nearest one.
        /// 
        /// It loops through the list and compares their respective distance to this object.
        /// When an object is nearer than the latest determined object, it sets the target to
        /// the new nearestTarget and saves it's distance to compare to the following objects.
        /// 
        /// After looping through the GameObject List it returns either the nearest GameObject
        /// or logs an error and returns null, if anything went wrong.
        /// </summary>

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

    private void ExitAndUpdateList(EnemyStateManager _enemy, GameObject _nearestObject)
    {
        /// <summary>
        /// When there is a nearest object in range, the function stops the roaming
        /// and sets the mainTarget in the StateManager to the nearest object.
        /// 
        /// It then removes the target object from the DetectedObjects List, to avoid
        /// null references.
        /// 
        /// Based on the enemies class (Screamer / "Normal"), the function decides
        /// if it should switch to the screaming or chasing state.
        /// </summary>
        /// 
        
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

    #endregion

}
