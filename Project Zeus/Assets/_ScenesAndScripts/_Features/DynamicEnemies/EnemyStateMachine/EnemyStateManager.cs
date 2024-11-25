using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
#region States
    public EnemyBaseState roamingState = new EnemyRoamingState();
    public EnemyChasingState chasingState = new EnemyChasingState();
    public EnemyAttackingState attackingState = new EnemyAttackingState();
#endregion
#region References
    EnemyBaseState currentState;
    Collider enemyDetectionRadius;
    GameObject mainTarget;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    #endregion

    #region Unity BuiltIn

    void Awake()
    {
        enemyDetectionRadius = GetComponent<SphereCollider>();
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
        if (CheckMethod("OnTriggerEnter"))
        {
            currentState.OnTriggerEnter(this, _collision);
        }
    }

    void OnTriggerExit(Collider _collision)
    {
        if (CheckMethod("OnTriggerExit"))
        {
            currentState.OnTriggerExit(this, _collision);
        }
    }

    public void OnUnitHit()
    {
        if (CheckMethod("OnUnitHit"))
        {
            currentState.OnUnitHit(this);
        }
    }

#endregion

#region Custom Functions

#region Public Functions
    public void SwitchState(EnemyBaseState _state)
    {
        currentState = _state;
        currentState.EnterState(this);
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
