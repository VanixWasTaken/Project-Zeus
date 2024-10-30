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

        commandCenter.buildingButton.SetActive(true);
        commandCenter.playerButton.SetActive(false);
        
    }

    public override void UpdateState(CommandCenterStateManager commandCenter)
    {
        // Switches states if you click somewhere else than the commandCenter
       /* if (inputActions.Mouse.Click.IsPressed() && !commandCenter.hoversAbove)
        {
            commandCenter.SwitchStates(commandCenter.idleState);
        }*/

    }

}