using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyStateManager : MonoBehaviour
{
    #region States

    public EnemyBaseState currentState; // currentstate
    public EnemyBaseState roamingState = new EnemyRoamingState();
    public EnemyChasingState chasingState = new EnemyChasingState();
    public EnemyAttackingState attackingState = new EnemyAttackingState();
    public EnemyRoamSoundState roamSoundState = new EnemyRoamSoundState();

    #endregion

    #region References

    // References that enable core functionality
    GameObject mainTarget;
    public NavMeshAgent navMeshAgent;
    public Animator animator;
    public GameObject spotLight;
    public UnitStateManager unitStateManager;

    public FMODAudioData audioSheet;

    #endregion

    #region Variables

    public bool unitSpotted = false;
    public Vector3 lastKnownUnitPosititon;
    public int health = 100;
    public bool shouldAttackUnits = false;
    public Vector3 lastHeardSoundPosition; // Everytime an enemy hears a shot he saves its position in this variable
    public bool finishedScream = false;

    [Header("Game Design Variables")]
    public float speed = 2f;
    public float chasingSpeed = 5f;
    public List<Vector3> patrolPoints; // points that make up the circle

    #endregion



    #region Unity BuiltIn

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        FMODUnity.RuntimeManager.LoadBank("ENEMIES");
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

        Die();
    }

    #region Colliders

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

    public void ShouldAttackUnits(Collider other, bool _shouldAttackUnits)
    {
        if (other.CompareTag("Worker") || other.CompareTag("Recon") || other.CompareTag("Fighter"))
        {
            shouldAttackUnits = _shouldAttackUnits;
            unitStateManager = other.GetComponent<UnitStateManager>();
        }
    }

    #endregion

    #region Animations

    public void OnUnitHit()
    {
        unitStateManager.health -= 25;
    }

    public void OnScreamFinished()
    {
        animator.SetTrigger("anShouldChase");
        finishedScream = true;
    }

    public void OnDeathAnimationFinished()
    {
        Destroy(gameObject);
    }


    #endregion

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

    public void TakeDamage(int _damage)
    {
        health -= _damage;
    }

    public void Die()
    {
        if (health <= 0)
        {
            animator.SetTrigger("anShouldDie");
        }
    }

    #endregion

}
