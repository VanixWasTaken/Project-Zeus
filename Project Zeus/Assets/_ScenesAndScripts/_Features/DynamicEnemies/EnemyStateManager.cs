using System.Collections;
using Unity.VisualScripting;
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
    #endregion

    #region Unity BuiltIn

    void Awake()
    {
        enemyDetectionRadius = GetComponent<SphereCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        else
        {
            Debug.LogError("Error: No mainTarget set in EnemyStateManager! The return value is null.");
            return null;
        }
    }

    public void SetTarget(GameObject _target)
    {
        mainTarget = _target;
        Debug.Log("Target set to:" + mainTarget);
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
        if (currentState == attackingState)
        {
            yield return new WaitForSeconds(_attackDelay);
            attackingState.AttackWorker(this, GetTarget());
        }
    }
        
    #endregion
}