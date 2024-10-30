using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using AudioType = UnityEngine.Audio.AudioType;

public class UnitStateManager : MonoBehaviour
{
    // All available PlayerStates
    #region PlayerStates
    UnitBaseState currentState;
    public UnitIdleState idleState = new UnitIdleState();
    public UnitWalkingState walkingState = new UnitWalkingState();
    public UnitFightState fightState = new UnitFightState();
    #endregion

    // All References
    #region References
    public Animator mAnimator;
    [SerializeField] Camera mainCamera;
    [SerializeField] Vector3 mouseClickPos;
    [SerializeField] CommandCenterStateManager commandCenter;
    public AudioController audioController;
    public NavMeshAgent navMeshAgent;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] GameObject selectionIndicator;
    public float life = 100;
    public int enemiesInRange = 0;
    public string myEnemyTag;
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
        if (life <= 0) 
        {
        Destroy(gameObject);
        }
    }

    public void SwitchStates(UnitBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void OnFootstep()
    {
        currentState.OnFootstep(this);
    }

    public void OnCommandMove(Vector3 targetPosition)
    {
        if (navMeshAgent != null)
        {
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
        if (currentState != fightState)
        {
            SwitchStates(idleState);
        }
        
    }


    public void Select()
    {
        // change visual to make selection apparent
        selectionIndicator.SetActive(true);
    }

    public void Deselect()
    {
        // change visual to make deselection apparent
        selectionIndicator.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(this, other);
    }

    public void WaitTimer(float waitTime)
    {
        StartCoroutine(WaitForSeconds(waitTime));
    }

    IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (currentState == fightState)
        {
            fightState.Fight(this);
        }
    }

}
