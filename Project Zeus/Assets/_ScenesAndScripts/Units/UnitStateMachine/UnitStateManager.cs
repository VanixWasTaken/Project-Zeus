using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;
using static UnityEngine.Audio.AudioType;

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

    public void OnMiningHit()
    {
        audioController.RandomizeAudioClip(DroneFarming_01, DroneFarming_03);
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // 7 = Resource Layer
        {
            mAnimator.SetTrigger("anShouldMine");
            mAnimator.SetBool("anIsMining", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 7) // 7 = Resource Layer
        {
            mAnimator.SetBool("anIsMining", false);
        }
    }
}
