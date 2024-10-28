using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

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
    public Camera mainCamera;
    public Vector3 mouseClickPos;
    public CommandCenterStateManager commandCenter;
    public AudioController audioController;
    public Vector3 targetPosition;
    private bool isSelected;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
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

    public void OnCommandMove(Vector3 destination)
    {
        targetPosition = destination;
        SwitchStates(walkingState);
    }

    public void Select()
    {
        isSelected = true;
        // change visual to make selection apparent
    }

    public void Deselect()
    {
        isSelected = false;
        // change visual to make deselection apparent
    }


    
}
