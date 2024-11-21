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
        HoversAboveButton(commandCenter);
        if (!commandCenter.hoversAbove)
        {
            if (Mouse.current.leftButton.isPressed && !aboveButton)
            {
                commandCenter.extracttionUnitInfo.gameObject.SetActive(false);
            }
        }


        commandCenter.extractionUIWorkers.text = "Workers:\t" + commandCenter.workersInsideExtraction.ToString() + " / "  + GameDataManager.Instance.pickedWorkers;
        commandCenter.extractionUIRecons.text = "Recons:\t" + commandCenter.reconsInsideExtraction.ToString() + " / " + GameDataManager.Instance.pickedRecons;
        commandCenter.extractionUIGatherers.text = "Gatherers:\t" + commandCenter.gatherersInsideExtraction.ToString() + " / " + GameDataManager.Instance.pickedGatherers;
    }



    void HoversAboveButton(CommandCenterStateManager commandCenter)
    {
        // Create PointerEventData
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

        // Raycast to detect UI elements
        List<RaycastResult> results = new List<RaycastResult>();
        GraphicRaycaster raycaster = commandCenter.extractionUIExtractButton.GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
        raycaster.Raycast(pointerEventData, results);

        // Check if the button is in the results
        aboveButton = results.Exists(result => result.gameObject == commandCenter.extractionUIExtractButton.gameObject || commandCenter.extractionWarningMenu.gameObject);
    }

}