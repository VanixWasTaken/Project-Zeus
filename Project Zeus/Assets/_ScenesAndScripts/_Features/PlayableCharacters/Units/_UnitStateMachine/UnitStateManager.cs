using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public UnitGathererFightingState gathererFightingState = new UnitGathererFightingState();
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
    public RightPartUIUnitDescription rightPartUIUnitDescription;
    #endregion

    // These values can be assigned in the Unit Prefabs, to make some Units more expensive than others
    public int energyDepletionRate = 5;
    public float energyDepletionInterval = 5;

    #region Worker Variables
    [Header("Worker Variables")]
    public int collectedEnergy;
    #endregion

    #region Gatherer Variables
    public int collectedLoot;

    public List<GameObject> enemiesInRange = new List<GameObject>();

    public GameObject mainTarget;
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
        animator.SetBool("isAttacking", false);
        StartCoroutine(EnergyDepletion(energyDepletionInterval));

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
        if (currentState != deactivatedState)
        {
            if (currentState != gathererFightingState)
            {
                navMeshAgent.SetDestination(targetPosition);
                SwitchStates(walkingState);
            }
        }
        
    }

    public void GathererShouldFight(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Screamer") || other.gameObject.CompareTag("Enemy"))
            {
                enemiesInRange.Add(other.gameObject);
                if (currentState != gathererFightingState)
                {
                    navMeshAgent.isStopped = true;
                    navMeshAgent.ResetPath();
                    SwitchStates(gathererFightingState);
                }
            }
        }
    }

    public void GathererRangeExited(Collider other)
    {
        if (other != null)
        {
            if (other.gameObject.CompareTag("Screamer") || other.gameObject.CompareTag("Enemy"))
            {
                enemiesInRange.Remove(other.gameObject);
                Debug.Log("I have removed: " + other.gameObject + " from the list!");
                if (enemiesInRange.Count <= 0)
                {
                    SwitchStates(idleState);
                }
            }
        }
    }

    public void OnEnemyHit()
    {
        if (currentState == gathererFightingState)
        {
            currentState.OnEnemyHit(this);
        }
    }

    public void Die()
    {

        UnitSelectionManager selectionManager = FindFirstObjectByType<UnitSelectionManager>();

        selectionManager.RemoveDestroyedUnits(this);

        Destroy(gameObject);
    }

    public void StopMoving() // Resets the NavMesh path and switches to idle state
    {
        navMeshAgent.ResetPath();
        SwitchStates(idleState);
    }

    public void TakeDamage(float _incomingDamage, EnemyStateManager _enemy)
    {
        if (tag == "Gatherer")
        {
            SwitchStates(gathererFightingState);
        }
        // very basic implementation, can be modified later
        life -= _incomingDamage;
    }

    public void EnergyLogic(string _action)
    {
        if (_action != null)
        {
            if (_action == "Reactivate")
            {
                if (currentState == deactivatedState)
                {
                    deactivatedState.Reactivate(this);
                }
            }
            else if(_action == "Deactivate")
            {
                if (currentState != deactivatedState)
                {
                    SwitchStates(deactivatedState);
                }
            }
        }
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
