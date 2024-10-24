using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityCore.Audio;
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
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && player.shouldMoveOnClick)
            {
                // Get the hit point in the 3D world
                Vector3 clickPosition = hit.point;

                player.mouseClickPos = clickPosition;

                RandomizeBarks(player);

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
                player.shouldMoveOnClick = false;
            }
        }
    }


    public void RandomizeBarks(PlayerStateManager player)
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(1, 4);


        if (num == 1)
        {
            player.audioController.PlayAudio(UnityCore.Audio.AudioType.SMAffirmBark_01);
        }
        else if (num == 2)
        {
            player.audioController.PlayAudio(UnityCore.Audio.AudioType.SMAffirmBark_02);
        }
        else if (num == 3)
        {
            player.audioController.PlayAudio(UnityCore.Audio.AudioType.SMAffirmBark_03);
        }
    }

}
