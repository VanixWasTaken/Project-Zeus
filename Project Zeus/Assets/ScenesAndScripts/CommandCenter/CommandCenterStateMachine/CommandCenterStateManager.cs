using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCenterStateManager : MonoBehaviour
{
    // All available CommandCenterStates
    #region PlayerStates
    CommandCenterBaseState currentState;
    public CommandCenterIdleState idleState = new CommandCenterIdleState();
    public CommandCenterClickedState clickedState = new CommandCenterClickedState();
    #endregion


    // All references
    #region References
    public Camera mainCamera;
    public GameObject commandCenterObject;
    #endregion


    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchStates(CommandCenterBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
