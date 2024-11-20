using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class CommandCenterClickedState : CommandCenterBaseState
{
    InputActions inputActions;



    public override void EnterState(CommandCenterStateManager commandCenter)
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();

    
    }

    public override void UpdateState(CommandCenterStateManager commandCenter)
    {
   
    }

}