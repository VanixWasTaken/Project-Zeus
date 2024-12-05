using TMPro.Examples;
using UnityEngine;
using UnityEngine.InputSystem;

public class DropshipIdleState : DropshipBaseState
{

    #region Reference

    InputActions inputActions;

    #endregion


    #region Unity Build In

    public override void EnterState(DropshipStateManager dropship)
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();
    }

    public override void UpdateState(DropshipStateManager dropship)
    {
        ClickedOnBase(dropship); // CURRENTLY DISABLED -- Switches states if you click on the dropship
    }

    #endregion



    #region Custom Functions()

    private void ClickedOnBase(DropshipStateManager dropship)
    {
        if (inputActions.Mouse.Click.IsPressed() && dropship.hoversAbove)
        {
            //commandCenter.SwitchStates(commandCenter.clickedState);
            //Debug.Log(commandCenter.collectedCompleteEnergy);
        }
    }

    #endregion
}



