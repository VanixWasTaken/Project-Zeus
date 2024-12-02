using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommandCenterIdleState : CommandCenterBaseState
{

    #region Reference

    InputActions inputActions;

    #endregion


    #region Unity Build In

    public override void EnterState(CommandCenterStateManager commandCenter)
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();
    }

    public override void UpdateState(CommandCenterStateManager commandCenter)
    {
        ClickedOnBase(commandCenter); // CURRENTLY DISABLED -- Switches states if you click on the commandCenter
    }

    #endregion



    #region Custom Functions()

    private void ClickedOnBase(CommandCenterStateManager commandCenter)
    {
        if (inputActions.Mouse.Click.IsPressed() && commandCenter.hoversAbove)
        {
            //commandCenter.SwitchStates(commandCenter.clickedState);
            //Debug.Log(commandCenter.collectedCompleteEnergy);
        }
    }

    #endregion
}



