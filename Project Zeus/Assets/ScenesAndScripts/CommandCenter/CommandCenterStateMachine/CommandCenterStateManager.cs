using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandCenterStateManager : MonoBehaviour
{
    // All available PlayerStates
    #region PlayerStates
    CommandCenterBaseState currentState;
    public CommandCenterIdleState idleState = new CommandCenterIdleState();
    public CommandCenterClickedState walkingState = new CommandCenterClickedState();
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
