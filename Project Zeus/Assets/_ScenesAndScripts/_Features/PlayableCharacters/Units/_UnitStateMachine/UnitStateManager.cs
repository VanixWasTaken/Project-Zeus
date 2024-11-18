using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitStateManager : MonoBehaviour
{
    #region UnitStates Variables
    public UnitBaseState currentState;
    public UnitIdleState idleState = new UnitIdleState();
    public UnitWalkingState walkingState = new UnitWalkingState();

    public UnitWorkerMiningState workerMiningState = new UnitWorkerMiningState();
    #endregion

    #region References Variables
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    GameObject selectionIndicator;
    //[SerializeField] Camera mainCamera;
    //[SerializeField] Vector3 mouseClickPos;
    //[SerializeField] CommandCenterStateManager commandCenter;
    //public ObjectAudioData audioSheet;
    //[SerializeField] Vector3 targetPosition;
    //[SerializeField] GameObject enemyDetector;
    //public float life = 100;
    //public List<GameObject> enemiesInRange;
    //public string myEnemyTag;
    //public int damage = 10;
    //public bool isDead = false;
    #endregion

    #region Worker Variables
    [Header("Worker Variables")]
    public int collectedEnergy;
    #endregion


    void Awake()
    {
        // Connects as many references per code as possible to hold the inspector clean
        #region Variable Connections
        if (navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            if (navMeshAgent == null)
            {
                Debug.LogError("NavMeshAgent could not be found and assigned");
            }
        }


        if (selectionIndicator == null)
        {
            foreach (Transform child in GetComponentsInChildren<Transform>())
            {
                if (child.gameObject.name == "SelectionIndicator")
                {
                    selectionIndicator = child.gameObject;
                    break;
                }
            }
            if (selectionIndicator == null)
            {
                Debug.LogError("SelectionIndicator could not be found and assigned");
            }
        }

        if (animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError("Animator could not be found and assigned");
            }
        }
        #endregion
    }


    void Start()
    {
        selectionIndicator.SetActive(false);
        currentState = idleState;
        currentState.EnterState(this);
    }


    void Update()
    {
        currentState.UpdateState(this);
    }



    #region Custom Functions()
    public void SwitchStates(UnitBaseState state) // Change the state
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void OnCommandMove(Vector3 targetPosition) // Moves the player to the set destination and switches to walking state
    {
        navMeshAgent.SetDestination(targetPosition);
        SwitchStates(walkingState);
    }

    public void StopMoving() // Resets the NavMesh path and switches to idle state
    {
        navMeshAgent.ResetPath();
        SwitchStates(idleState);
    }

    #region SelectionIndicator Functions()
    public void Select() // Change visual to make selection apparent.
    {
        selectionIndicator.SetActive(true);
    }
    public void Deselect() // Change visual to make deselection apparent
    {
        selectionIndicator.SetActive(false);
    }
    #endregion

    #endregion
}
