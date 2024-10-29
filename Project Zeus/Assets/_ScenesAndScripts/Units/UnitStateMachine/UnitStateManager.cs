using System.Collections;
using System.Collections.Generic;
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

    void SwitchStates(UnitBaseState state)
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
        SwitchStates(idleState);
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


    
}
