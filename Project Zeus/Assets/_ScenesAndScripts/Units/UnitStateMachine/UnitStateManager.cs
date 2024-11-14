using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.SceneManagement;
using static UnityEngine.UI.CanvasScaler;
using static FMODUnity.RuntimeManager;

public class UnitStateManager : MonoBehaviour
{
    // All available PlayerStates
    #region PlayerStates
    public UnitBaseState currentState;
    public UnitIdleState idleState = new UnitIdleState();
    public UnitWalkingState walkingState = new UnitWalkingState();
    public UnitFightState fightState = new UnitFightState();
    public UnitMiningState miningState = new UnitMiningState();
    #endregion

    // All References
    #region References
    public Animator mAnimator;
    [SerializeField] Camera mainCamera;
    [SerializeField] Vector3 mouseClickPos;
    [SerializeField] CommandCenterStateManager commandCenter;
    public ObjectAudioData audioSheet;
    public NavMeshAgent navMeshAgent;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] GameObject selectionIndicator;
    [SerializeField] GameObject enemyDetector;
    public float life = 100;
    public List<GameObject> enemiesInRange;
    public string myEnemyTag;
    public int damage = 10;
    public bool isDead = false;
    #endregion

    
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

    public void SwitchStates(UnitBaseState state)
    {
        if (!isDead)
        {
            if (state == idleState && tag == "Enemy")
            {
                GetComponent<BasicEnemyAI>().UpdateList();
            }
            currentState = state;
            state.EnterState(this);
        }
    }

    public void OnFootstep()
    {
        currentState.OnFootstep(this);
    }

    public void OnMiningHit()
    {
        PlayOneShot(audioSheet.exampleReference01);
    }

    public void OnCommandMove(Vector3 targetPosition)
    {
        if (navMeshAgent != null)
        {
            if (currentState == fightState)
            {
                mAnimator.SetBool("isAttacking", false);
            }
            if (currentState == miningState) 
            {
                mAnimator.SetBool("anIsMining", false);
            }


            navMeshAgent.SetDestination(targetPosition); // Use NavMeshAgent to move
            SwitchStates(walkingState); // Switch to walking state
        }
    }

    public void StopMoving()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.ResetPath();
        }
        if (currentState != fightState && currentState != miningState)
        {
            SwitchStates(idleState);
        }
        
    }

    public void Select()
    {
        if (!isDead && tag != "Enemy")
        {
            // change visual to make selection apparent
            selectionIndicator.SetActive(true);
        }

    }

    public void Deselect()
    {
        // change visual to make deselection apparent
        selectionIndicator.SetActive(false);
    }

    public void WaitTimer(float waitTime)
    {
        if (!isDead)
        {
            StartCoroutine(WaitForSeconds(waitTime));
        }
    }

    IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (currentState == fightState)
        {
            
            fightState.Fight(this);
        }
    }

    public void TakeDamage(int incomingDamage)
    {
        life -= incomingDamage;
        if (life <= 0)
        {
            tag = "Untagged";
            mAnimator.SetTrigger("shouldDie");
            isDead = true;
            Destroy(GetComponent<Rigidbody>());
        }
    }
}
