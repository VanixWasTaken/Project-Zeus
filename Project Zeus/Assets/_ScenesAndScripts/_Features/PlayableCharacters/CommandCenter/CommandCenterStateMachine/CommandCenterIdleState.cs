using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommandCenterIdleState : CommandCenterBaseState
{
    InputActions inputActions;

    

    public override void EnterState(CommandCenterStateManager commandCenter)
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();
    }

    public override void UpdateState(CommandCenterStateManager commandCenter)
    {
        // Switches states if you click on the commandCenter
        if (inputActions.Mouse.Click.IsPressed() && commandCenter.hoversAbove)
        {
            //commandCenter.SwitchStates(commandCenter.clickedState);
            //Debug.Log(commandCenter.collectedCompleteEnergy);
        }
    }
}



