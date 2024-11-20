using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class UnitStateManager : MonoBehaviour
{
    #region UnitStates Variables
    public UnitBaseState currentState;
    public UnitIdleState idleState = new UnitIdleState();
    public UnitWalkingState walkingState = new UnitWalkingState();
    public UnitDeactivatedState deactivatedState = new UnitDeactivatedState();

    public UnitWorkerMiningState workerMiningState = new UnitWorkerMiningState();
    #endregion

    #region References Variables
    public Animator animator;
    public NavMeshAgent navMeshAgent;
    GameObject selectionIndicator;
    //[SerializeField] Camera mainCamera;
    //[SerializeField] Vector3 mouseClickPos;
    CommandCenterStateManager commandCenter; // needed to call function DepleteEnergy()
    //public ObjectAudioData audioSheet;
    //[SerializeField] Vector3 targetPosition;
    //[SerializeField] GameObject enemyDetector;
    public float life = 100; // delte later
    //public List<GameObject> enemiesInRange;
    //public string myEnemyTag;
    //public int damage = 10;
    //public bool isDead = false;
    #endregion

    // These values can be assigned in the Unit Prefabs, to make some Units more expensive than others
    public int energyDepletionRate = 5;
    public float energyDepletionInterval = 5;

    #region Worker Variables
    [Header("Worker Variables")]
    public int collectedEnergy;
    #endregion

    public InputActions newInputActions;

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

        if (commandCenter == null)
        {
            GameObject[] cC = GameObject.FindGameObjectsWithTag("CommandCenter");

            foreach (GameObject c in cC)
            {
                if (c != null)
                {
                    commandCenter = c.GetComponent<CommandCenterStateManager>();
                }
            }

            if (commandCenter == null)
            {
                Debug.LogError("CommandCenter could not be found and assigned");
            }
        }
        #endregion
    }


    void Start()
    {
        newInputActions = new InputActions();
        newInputActions.Keyboard.Enable();

        StartCoroutine(EnergyDepletion(energyDepletionInterval));

        selectionIndicator.SetActive(false);
        currentState = idleState;
        currentState.EnterState(this);
    }


    void Update()
    {
        currentState.UpdateState(this);

        if (newInputActions.Keyboard.DeactiveUnit.WasPressedThisFrame())
        {
            if (currentState != deactivatedState)
            {
                SwitchStates(deactivatedState);
            }
            
        }
    }



    #region Custom Functions()
    public void SwitchStates(UnitBaseState state) // Change the state
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void OnCommandMove(Vector3 targetPosition) // Moves the player to the set destination and switches to walking state
    {
        if (currentState != deactivatedState)
        {
            navMeshAgent.SetDestination(targetPosition);
            SwitchStates(walkingState);
        }
        
    }

    public void StopMoving() // Resets the NavMesh path and switches to idle state
    {
        navMeshAgent.ResetPath();
        SwitchStates(idleState);
    }

    public void TakeDamage()
    {
        // Write Function to take damage here later
    }

    private void DepleteEnergy(int _amount)
    {
        commandCenter.DepleteEnergy(_amount);
    }

    public IEnumerator EnergyDepletion(float _depletionInterval)
    {
        while (true)
        {
            yield return new WaitForSeconds(_depletionInterval);

            DepleteEnergy(energyDepletionRate);
        }
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
