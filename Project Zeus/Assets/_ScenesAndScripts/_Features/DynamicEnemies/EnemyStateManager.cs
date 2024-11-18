using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyBaseState roamingState = new EnemyRoamingState();
    public EnemyBaseState chasingState = new EnemyChasingState();
    public EnemyBaseState attackingState = new EnemyAttackingState();

    EnemyBaseState currentState;

    Collider enemyDetectionRadius;

    GameObject mainTarget;

    void Awake()
    {
        enemyDetectionRadius = GetComponent<SphereCollider>();
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

    public void SwitchState(EnemyBaseState _state, GameObject _target)
    {
        currentState = _state;
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
}
