using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static FMODUnity.RuntimeManager;

public class UnitStateManager : MonoBehaviour
{
    #region UnitStates

    public UnitBaseState currentState;
    public UnitIdleState idleState = new UnitIdleState();
    public UnitWalkingState walkingState = new UnitWalkingState();
    public UnitDeactivatedState deactivatedState = new UnitDeactivatedState();
    public UnitWorkerMiningState workerMiningState = new UnitWorkerMiningState();
    public UnitFightingState fightingState = new UnitFightingState();

    #endregion

    #region References

    public Animator animator;
    public NavMeshAgent navMeshAgent;
    GameObject selectionIndicator;
    DropshipStateManager dropship; // needed to call function DepleteEnergy()
    public FMODAudioData audioSheet;
    public RightPartUIUnitDescription rightPartUIUnitDescription;
    public EnemyStateManager enemyStateManager;

    #endregion

    #region Variables

    public enum UnitClass
    {
        Worker,
        Recon,
        Fighter
    }
    UnitClass unitClass;
    public float health;
    private float movementSpeed;
    private float visionRange;
    private float attackRange;
    private float attackSpeed;
    private int attackDamage;
    private float carryingCapacity;
    public int energyDepletionRate; // Can be assigned in the Unit Prefabs, to make some Units more expensive than others
    public float energyDepletionInterval = 1; // Can be assigned in the Unit Prefabs, to make some Units more expensive than others
    public Vector3 nearestEnemyPosition;

    [Header("Worker Variables")]
    public int collectedEnergy;

    [Header("Fighter Variables")]
    public int collectedLoot;
    public List<GameObject> enemiesInRange = new List<GameObject>();
    public GameObject mainTarget;


    #endregion


    #region Unity Build In

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

        if (dropship == null)
        {
            GameObject[] cC = GameObject.FindGameObjectsWithTag("Dropship");

            foreach (GameObject c in cC)
            {
                if (c != null)
                {
                    dropship = c.GetComponent<DropshipStateManager>();
                }
            }

            if (dropship == null)
            {
                Debug.LogError("Dropship could not be found and assigned");
            }
        }

        LoadBank("UNIT");

        #endregion

        #region Set Class

        if (CompareTag("Worker"))
        {
            SetClass(UnitClass.Worker);
        }
        else if (CompareTag("Recon"))
        {
            SetClass(UnitClass.Recon);
        }
        else if (CompareTag("Fighter"))
        {
            SetClass(UnitClass.Fighter);
        }

        #endregion
    }


    void Start()
    {
        StartCoroutine(EnergyDepletion(energyDepletionInterval));

        selectionIndicator.SetActive(false);
        currentState = idleState;
        currentState.EnterState(this);

        #region Define Class

        if (unitClass == UnitClass.Worker)
        {
            health = 100;
            movementSpeed = 5;
            visionRange = 3;
            attackRange = 3;
            attackSpeed = 2;
            attackDamage = 25;
            carryingCapacity = 100;
            energyDepletionRate = 1;
        }
        else if (unitClass == UnitClass.Recon)
        {
            health = 50;
            movementSpeed = 8;
            visionRange = 7;
            attackRange = 7;
            attackSpeed = 1;
            attackDamage = 100;
            carryingCapacity = 0;
            energyDepletionRate = 2;
        }
        else if (unitClass == UnitClass.Fighter)
        {
            health = 200;
            movementSpeed = 2;
            visionRange = 5;
            attackRange = 5;
            attackSpeed = 3;
            attackDamage = 50;
            carryingCapacity = 0;
            energyDepletionRate = 4;
        }

        #endregion
    }


    void Update()
    {
        currentState.UpdateState(this);

        if (health <= 0)
        {
            Die();
        }
    }

    #region Animator

    public void OnFootstep()
    {
        //PlayOneShot();
    }

    public void OnShooting()
    {
        enemyStateManager.TakeDamage(attackDamage);
        Debug.Log("Damage: " + attackDamage);
    }

    #endregion

    #endregion



    #region Custom Functions()

    public void SwitchStates(UnitBaseState state) // Change the state
    {
        currentState = state;
        currentState.EnterState(this);
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

    public void Die()
    {

        UnitSelectionManager selectionManager = FindFirstObjectByType<UnitSelectionManager>();

        selectionManager.RemoveDestroyedUnits(this);

        Destroy(gameObject);
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

    public void SetClass(UnitClass _unitclass) // Sets the current class to the intended unit
    {
        unitClass = _unitclass;
        Debug.Log("UnitClass set  to " + unitClass);
    }

    private void DepleteEnergy(int _amount)
    {
        dropship.DepleteEnergy(_amount);
    }

    public void ScanForEnemies(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyStateManager = other.GetComponent<EnemyStateManager>();
            nearestEnemyPosition = other.transform.position; // Saves the last position of the enemy for uses like view direction or target priority
            SwitchStates(fightingState);
        }
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
