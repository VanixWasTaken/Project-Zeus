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

        commandCenter.commandCenterHUD.SetActive(true);
    }

    public override void UpdateState(CommandCenterStateManager commandCenter)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = commandCenter.mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        int layerMask = 1 << 6;

        bool raycastHit = Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

        if (raycastHit)
        {
            commandCenter.commandCenterObject.layer = LayerMask.NameToLayer("Outline");
            commandCenter.hoversAbove = true;
        }
        else
        {
            commandCenter.commandCenterObject.layer = LayerMask.NameToLayer("Default");
            commandCenter.hoversAbove = false;
        }

    }

}