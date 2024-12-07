using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyStateManager : MonoBehaviour
{
    #region States

    EnemyBaseState currentState; // currentstate
    public EnemyBaseState roamingState = new EnemyRoamingState();
    public EnemyChasingState chasingState = new EnemyChasingState();
    public EnemyAttackingState attackingState = new EnemyAttackingState();

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

    #endregion

    #region Variables

    public List<Vector3> patrolPoints; // points that make up the circle
    public bool unitSpotted = false;
    public Vector3 lastKnownUnitPosititon;

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


    #endregion


    #region Custom Functions

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

    public void Die()
    {
        Destroy(gameObject);
    }

    public void DetectUnits(Collider other)
    {
        if (other.CompareTag("Worker") || other.CompareTag("Recon") || other.CompareTag("Fighter"))
        {
            unitSpotted = true;
            lastKnownUnitPosititon = other.transform.position;
        }
    }

    public void LooseUnits(Collider other)
    {
        if (other.CompareTag("Worker") || other.CompareTag("Recon") || other.CompareTag("Fighter"))
        {
            unitSpotted = false;
            lastKnownUnitPosititon = other.transform.position;
        }
    }



    #endregion

}
