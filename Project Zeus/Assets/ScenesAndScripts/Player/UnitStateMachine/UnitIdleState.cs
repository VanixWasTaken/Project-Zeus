using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.InputSystem;
using AudioType = UnityEngine.Audio.AudioType;

public class UnitIdleState : UnitBaseState
{
    public override void EnterState(UnitStateManager _unit)
    {
        _unit.mAnimator.SetFloat("anSpeed", 0);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        /*
        if (inputActions.Mouse.Click.IsPressed())
        {
            // Create a ray from the camera going through the mouse position
            Ray ray = _unit.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            RaycastHit hit;

            // perfoming the raycast
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) && _unit.shouldMoveOnClick && !_unit.mouseAboveUI)
            {
                // Get the hit point in the 3D world
                Vector3 clickPosition = hit.point;

                _unit.mouseClickPos = clickPosition;

                _unit.audioController.RandomizeAudioClip(AudioType.SMAffirmBark_01, AudioType.SMAffirmBark_03);

                _unit.SwitchStates(_unit.walkingState);
            }
        }

        
        Ray ray2 = _unit.mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit2;

        
        if (Physics.Raycast(ray2, out hit2, Mathf.Infinity))
        {
            int hitLayer = hit2.collider.gameObject.layer;

            if (LayerMask.LayerToName(hitLayer) == "UI")
            {
                _unit.mouseAboveUI = true;
            }
            else { _unit.mouseAboveUI = false; }
        }
        */
    }
}
