using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyStateManager : MonoBehaviour
{
    #region States

    public EnemyBaseState roamingState = new EnemyRoamingState();
    public EnemyChasingState chasingState = new EnemyChasingState();
    public EnemyAttackingState attackingState = new EnemyAttackingState();

    // Special State for the Screamer enemy
    public EnemyScreamerScreamingState screamerScreamingState = new EnemyScreamerScreamingState();

    EnemyBaseState currentState; // currentstate

    #endregion

    #region References

    // References that enable core functionality
    GameObject mainTarget;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public GameObject spotLight;

    // List that contains references to objects in range
    private List<GameObject> detectedObjects = new List<GameObject>();
    private List<GameObject> enemiesInRange = new List<GameObject>();

    // references used for the roaming behaviour
    public Transform centerPoint; // center of the circular path
    public Vector3[] patrolPoints; // points that make up the circle
    public Vector3 circleCenter;

    #endregion

    #region Variables

    // should the player return to roaming or chasing state?
    public bool shouldPatrol = false;

    // variables used to create the circular path for roaming
    public float radius = 20f;
    public float patrolSpeed = 3f;
    public int segments = 24; // segments of the path: more = more circular
    public int currentPointIndex = 0;

    // variables for fighting
    public int life = 50;

    #endregion



    #region Unity BuiltIn

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        spotLight.SetActive(false);
        currentState = roamingState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }


    #region Colliders

    private void OnTriggerEnter(Collider _collision)
    {
        if (_collision.CompareTag("Enemy") || _collision.CompareTag("Screamer"))
        {
            enemiesInRange.Add(_collision.gameObject);
        }
        else if (_collision.gameObject.layer == 15)
        {
            detectedObjects.Add(_collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider _collision)
    {
        if (_collision.CompareTag("Enemy") || _collision.CompareTag("Screamer"))
        {
            enemiesInRange.Remove(_collision.gameObject);
        }
        else if (_collision.gameObject.layer == 15)
        {
            detectedObjects.Remove(_collision.gameObject);
        }
    }

    #endregion


    #region Animator

    public void OnUnitHit()
    {
        if (CheckMethod("OnUnitHit"))
        {
            currentState.OnUnitHit(this);
        }
    }

    public void OnScreamHeard()
    {
        if (CheckMethod("OnScreamHeard"))
        {
            currentState.OnScreamHeard(this);
        }
    }

    public void OnScreamFinished()
    {
        if (CheckMethod("OnScreamFinished"))
        {
            currentState.OnScreamFinished(this);
        }
    }

    #endregion

    #endregion


    #region Custom Functions

    private bool CheckMethod(string funcName)
    {
        var thisType = currentState.GetType();

        if (thisType.GetMethod(funcName) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SwitchState(EnemyBaseState _state)
    {
        currentState = _state;
        currentState.EnterState(this);
    }

    public void ActivateLight()
    {
        if (!spotLight.activeSelf)
        {
            spotLight.SetActive(true);
        }
    }

    public void DeactivateLight()
    {
        if (spotLight.activeSelf)
        {
            spotLight.SetActive(false);
        }
    }

    public void MoveToTarget(Vector3 targetPosition)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(targetPosition); // Use NavMeshAgent to move
        }
    }

    public void TakeDamage(UnitStateManager _unit, int _damage)
    {
        life -= _damage;
        mainTarget = _unit.gameObject;
        SwitchState(chasingState);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void HearScream(GameObject _target)
    {
        StopAllCoroutines();
        navMeshAgent.isStopped = true;
        navMeshAgent.ResetPath();
        SetTarget(_target);
        SwitchState(chasingState);
    }

    public void UpdateDetectedObjects(GameObject _object)
    {
        detectedObjects.Remove(_object);
    }

    public IEnumerator RoamingBehaviour()
    {
        while (true)
        {
            // Move to the current patrol point
            navMeshAgent.SetDestination(patrolPoints[currentPointIndex]);

            // Wait until the agent reaches the destination
            while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
            {
                yield return null;
            }

            // Move to the next point
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }


    #region Get-Set-Functions()

    public List<GameObject> GetDetectedObjects()
    {
        if (detectedObjects != null)
        {
            return detectedObjects;
        }
        else
        {
            return null;
        }
    }

    public List<GameObject> GetEnemiesInRange()
    {
        if (enemiesInRange != null)
        {
            return enemiesInRange;
        }
        else
        {
            return null;
        }
    }

    public GameObject GetTarget() 
    {
        if (mainTarget != null)
        {
            return mainTarget;
        }
        else if (mainTarget == null)
        {
            SwitchState(roamingState);
            return mainTarget;
        }
        else
        {
            Debug.LogError("Error: No mainTarget set in EnemyStateManager! The return value is null.");
            return null;
        }
    }

    public void SetTarget(GameObject _target)
    {
        if (_target == null)
        {
            mainTarget = null;
        }
        else if (_target != null)
        {
            mainTarget = _target;
            Debug.Log("Target set to:" + mainTarget);
        }
    }

    #endregion

    #endregion

}
