using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class CommandCenterClickedState : CommandCenterBaseState
{
    InputActions inputActions;

    bool aboveButton = false;

    public override void EnterState(CommandCenterStateManager commandCenter)
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();

        commandCenter.extracttionUnitInfo.gameObject.SetActive(true);
    }

    public override void UpdateState(CommandCenterStateManager commandCenter)
    {
        if (!commandCenter.hoversAbove)
        {
            if (Mouse.current.leftButton.isPressed && !aboveButton)
            commandCenter.extracttionUnitInfo.gameObject.SetActive(false);
        }
    }

}