using UnityEngine;
using UnityEngine.InputSystem;

public class CommandCenterIdleState : CommandCenterBaseState
{
    InputActions inputActions;
    bool hoversAbove = false;

    

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
            //commandCenter.commandCenterObject.
        }
        else
        {
            Debug.Log("Raycast hat nichts getroffen.");
        }

        if (raycastHit)
        {
            hoversAbove = true;
        }
        else { hoversAbove = false; }


        if (inputActions.Mouse.Click.IsPressed() && hoversAbove)
        {
            Debug.Log("Ich wurde angeklickt: " + hit.collider.gameObject.name);
        }
    }
}



