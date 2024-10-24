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
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = commandCenter.mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        int layerMask = 1 << 6;

        bool raycastHit = Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);

        if (raycastHit)
        {
            commandCenter.commandCenterObject.layer = LayerMask.NameToLayer("Outline");
        }
        else
        {
            commandCenter.commandCenterObject.layer = LayerMask.NameToLayer("Default");
        }

        if (raycastHit)
        {
            commandCenter.hoversAbove = true;
        }
        else { commandCenter.hoversAbove = false; }


        if (inputActions.Mouse.Click.IsPressed() && commandCenter.hoversAbove)
        {
            commandCenter.commandCenterHUD.SetActive(true);
        }
    }
}



