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

    public EnemyScreamerScreamingState screamerScreamingState = new EnemyScreamerScreamingState();
    #endregion
    #region References
    EnemyBaseState currentState;
    GameObject mainTarget;
    public NavMeshAgent navMeshAgent;
    public Animator animator;

    private List<GameObject> detectedObjects = new List<GameObject>();
    private List<GameObject> enemiesInRange = new List<GameObject>();

    #endregion

    #region Unity BuiltIn

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentState = roamingState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnTriggerEnter(Collider _collision)
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

    void OnTriggerExit(Collider _collision)
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

    #region Animator Events
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

    #region Public Functions
    public void SwitchState(EnemyBaseState _state)
    {
        currentState = _state;
        currentState.EnterState(this);
    }

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

    public void UpdateDetectedObjects(GameObject _object)
    {
        detectedObjects.Remove(_object);
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
            return default;
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

    public void MoveToTarget(Vector3 targetPosition)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.SetDestination(targetPosition); // Use NavMeshAgent to move
        }
    }

    public void HearScream(GameObject _target)
    {
        Debug.Log("I have heard the scream");
        SetTarget(_target);
        SwitchState(chasingState);
    }

    #endregion

    bool CheckMethod(string funcName)
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

    public IEnumerator AttackDelay(float _attackDelay)
    {
        Debug.Log("Starting Attack Delay");
        if (currentState == attackingState)
        {
            yield return new WaitForSeconds(_attackDelay);
            attackingState.AttackWorker(this, GetTarget());
        }
    }
        
    #endregion
}