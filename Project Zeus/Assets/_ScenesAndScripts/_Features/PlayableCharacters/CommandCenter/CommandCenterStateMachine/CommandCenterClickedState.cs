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
        inputActions = new InputActions(); // i disabled the transition to this state since this causes memory leaks
        inputActions.Mouse.Enable();       // and there are no exit conditions, so we just enable mouse controls for no reason

        //Debug.Log(commandCenter.collectedCompleteEnergy);
    }

    public override void UpdateState(CommandCenterStateManager commandCenter)
    {
   
    }

}