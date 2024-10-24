using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerIdleState : PlayerBaseState
{
    
    InputActions inputActions;

    public override void EnterState(PlayerStateManager player)
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();

        player.mAnimator.SetFloat("anSpeed", 0);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        if (inputActions.Mouse.Click.IsPressed())
        {
            // Create a ray from the camera going through the mouse position
            Ray ray = player.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            RaycastHit hit;

            // perfoming the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && player.commandCenter.hoversAbove == false)
            {
                // Get the hit point in the 3D world
                Vector3 clickPosition = hit.point;

                player.mouseClickPos = clickPosition;

                player.SwitchStates(player.walkingState);
            }
        }



    }
}
