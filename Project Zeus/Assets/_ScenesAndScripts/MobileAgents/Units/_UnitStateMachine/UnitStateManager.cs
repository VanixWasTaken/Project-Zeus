using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static FMODUnity.RuntimeManager;
using static FMODAudioData.SoundID;
using FischlWorks_FogWar;

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
    public RightPartUIUnitDescription rightPartUIUnitDescription;
    public EnemyStateManager enemyStateManager;
    private csFogWar fogWar;

    #region Sound References

    public FMODAudioData audioSheet;

    private FMOD.Studio.EventInstance shooting;
    private FMOD.Studio.EventInstance moving;

    #endregion

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
    public int visionRange;
    public int visionConeRange;
    private float attackRange;
    private float attackSpeed;
    private int attackDamage;
    private float carryingCapacity;
    public int energyDepletionRate; // Can be assigned in the Unit Prefabs, to make some Units more expensive than others
    public float soundEmittingRange; // Defines the range in which the units emit sound
    public Vector3 nearestEnemyPosition;
    public GameObject shootingSoundGO; // A big sphere that represents the range the shooting sound is heard by other enemies, allerting them to roam the area where the sound was
    public GameObject visionConeGO; // A BoxCollider that represents the view distance

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
        #region Reference Connections
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

        if (fogWar == null) // Connects the Fog of War plugin
        {
            fogWar = FindAnyObjectByType<csFogWar>();

            if (fogWar == null)
            {
                Debug.LogError("fogWar on UnitStateManager could not found and assigned");
            }
        }

          

        LoadBank("UNIT");

        #endregion

        #region Set Class

        if (CompareTag("Worker"))
        {
            SetClass(UnitClass.Worker);
            LoadBank("WORKER");
        }
        else if (CompareTag("Recon"))
        {
            SetClass(UnitClass.Recon);
            LoadBank("RECON");
        }
        else if (CompareTag("Fighter"))
        {
            SetClass(UnitClass.Fighter);
            LoadBank("FIGHTER");
        }

        #endregion
    }


    void Start()
    {
        StartCoroutine(EnergyDepletion()); // Start the energy depletion (once per second updated)

        selectionIndicator.SetActive(false);
        currentState = idleState;
        currentState.EnterState(this);

        #region Define Class

        if (unitClass == UnitClass.Worker)
        {
            health = 100;
            movementSpeed = 5;
            visionRange = 6;
            visionConeRange = 3;
            attackRange = 3;
            attackSpeed = 2;
            attackDamage = 25;
            carryingCapacity = 100;
            energyDepletionRate = 1;
            soundEmittingRange = 30;
            
            fogWar.AddFogRevealer(new csFogWar.FogRevealer(this.transform, visionRange, true)); // Set the Fog of War Revealer
        }
        else if (unitClass == UnitClass.Recon)
        {
            health = 50;
            movementSpeed = 8;
            visionRange = 14;
            visionConeRange = 7;
            attackRange = 7;
            attackSpeed = 1;
            attackDamage = 100;
            carryingCapacity = 0;
            energyDepletionRate = 2;
            soundEmittingRange = 30;

            fogWar.AddFogRevealer(new csFogWar.FogRevealer(this.transform, visionRange, true)); // Set the Fog of War Revealer
        }
        else if (unitClass == UnitClass.Fighter)
        {
            health = 200;
            movementSpeed = 2;
            visionRange = 10;
            visionConeRange = 5;
            attackRange = 5;
            attackSpeed = 3;
            attackDamage = 50;
            carryingCapacity = 0;
            energyDepletionRate = 4;
            soundEmittingRange = 10;

            fogWar.AddFogRevealer(new csFogWar.FogRevealer(this.transform, visionRange, true)); // Set the Fog of War Revealer
        }

        SetAllClassStats(); // After adjusting the values above some stats need to be actally applied to its references, this happens here 

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
        EmitShootingSound(true);
        Debug.Log("Damage: " + attackDamage);
    }

    public void OnShootSoundActivated()
    {
        EmitShootingSound(false);
    }

    #endregion

    #region Colliders

    public void ScanForEnemies(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyStateManager = other.GetComponent<EnemyStateManager>();
            nearestEnemyPosition = other.transform.position; // Saves the last position of the enemy for uses like view direction or target priority
            SwitchStates(fightingState);
        }
    }

    public void AllertEnemiesInSoundRange(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyStateManager _enemyStateManager = other.GetComponent<EnemyStateManager>();

            _enemyStateManager.lastHeardSoundPosition = transform.position;
            if (_enemyStateManager.currentState != _enemyStateManager.attackingState || _enemyStateManager.currentState != _enemyStateManager.chasingState)
            {
                _enemyStateManager.SwitchState(_enemyStateManager.roamSoundState);
            }
        }
    }

    #endregion

    #endregion



    #region Custom Functions()

    public void SwitchStates(UnitBaseState state) // Change the state
    {
        currentState = state;
        currentState.EnterState(this);
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

    private void EmitShootingSound(bool _setActive) // Is used to activated a big sphere for a split second, after a bullet is shot, to allert enemies in range to investigate the area (faking that they heard the shot)
    {
        shootingSoundGO.SetActive(_setActive);
    } 

    private void SetAllClassStats()
    {
        // Set the size of the sphere that represents the shooting sound range that can be heard by enemies
        SphereCollider shootingSoundSphere = shootingSoundGO.GetComponent<SphereCollider>();
        shootingSoundSphere.radius = soundEmittingRange;

        // Set the size of the cube vision cone infront of the unit, that represents the view distance
        BoxCollider visionCone = visionConeGO.GetComponent<BoxCollider>();
        visionCone.size = new Vector3(5, 2, visionRange);
        visionCone.transform.position += new Vector3(0, 0, visionRange / 2);
    }

    public IEnumerator EnergyDepletion()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);

            DepleteEnergy(energyDepletionRate);
        }
    }

    #region Sound Functions()

    public void PlayShooting()
    {
        if (!IsPlaying(shooting))
        {
            if (unitClass == UnitClass.Fighter)
            {
                shooting = CreateInstance(audioSheet.GetSFXByName(SFXUnitFighterShooting));
                shooting.setParameterByName("Firing", 0);
            }
            else if (unitClass == UnitClass.Recon)
            {
                shooting = CreateInstance(audioSheet.GetSFXByName(SFXUnitReconShot));
            }

            shooting.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            shooting.start();
            shooting.release();
        }
    }

    public void LetShootingFinish()
    {
        if (IsPlaying(shooting)) 
        {
            shooting.setParameterByName("Firing", 1);
            shooting.release();
        }
        
    }

    public void PlayMoving()
    {
        if (!IsPlaying(moving))
        {
            if (unitClass == UnitClass.Fighter)
            {
                moving = CreateInstance(audioSheet.GetSFXByName(SFXUnitFighterMoving));
                moving.setParameterByName("Moving", 0);
            }
            else if (unitClass == UnitClass.Recon)
            {
                //moving = CreateInstance(audioSheet.GetSFXByName(SFXUnitReconShot));
            }

            moving.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            moving.start();
            moving.release();
        }
    }

    public void LetMovingFinish()
    {
        if (IsPlaying(moving))
        {
            moving.setParameterByName("Moving", 1);
            moving.release();
        }
    }

    private bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        instance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }

    #endregion

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
