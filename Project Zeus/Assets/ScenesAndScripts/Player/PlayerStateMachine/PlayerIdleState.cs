using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using AudioType = UnityEngine.Audio.AudioType;

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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && player.shouldMoveOnClick && !player.mouseAboveUI)
            {
                // Get the hit point in the 3D world
                Vector3 clickPosition = hit.point;

                player.mouseClickPos = clickPosition;

                player.audioController.RandomizeAudioClip(AudioType.SMAffirmBark_01, AudioType.SMAffirmBark_03);

                player.SwitchStates(player.walkingState);
            }
        }

        
        Ray ray2 = player.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit2;

        
        if (Physics.Raycast(ray2, out hit2, Mathf.Infinity))
        {
            int hitLayer = hit2.collider.gameObject.layer;

            if (LayerMask.LayerToName(hitLayer) == "UI")
            {
                player.mouseAboveUI = true;
            }
            else { player.mouseAboveUI = false; }
        }
    }
}
