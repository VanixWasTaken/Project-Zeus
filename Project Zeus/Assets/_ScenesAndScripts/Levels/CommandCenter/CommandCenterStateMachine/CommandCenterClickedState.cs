using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class CommandCenterClickedState : CommandCenterBaseState
{

    #region References

    private InputActions inputActions;

    #endregion

    #region Variables

    private bool aboveButton = false;

    #endregion


    #region Unity Build In

    public override void EnterState(CommandCenterStateManager commandCenter)
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();

        commandCenter.extracttionUnitInfo.gameObject.SetActive(true);
    }

    public override void UpdateState(CommandCenterStateManager commandCenter)
    {
        HoversAboveButton(commandCenter);

        ClickedOnBase(commandCenter);

        UpdateText(commandCenter);
    }

    #endregion



    #region Custom Function()

    private void HoversAboveButton(CommandCenterStateManager commandCenter) // Checks If the mouse currently is above a Button
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

    private void ClickedOnBase(CommandCenterStateManager commandCenter)
    {
        if (!commandCenter.hoversAbove)
        {
            if (Mouse.current.leftButton.isPressed && !aboveButton)
            {
                commandCenter.extracttionUnitInfo.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateText(CommandCenterStateManager commandCenter)
    {
        commandCenter.extractionUIWorkers.text = "Workers:\t" + commandCenter.workersInsideExtraction.ToString() + " / " + GameDataManager.Instance.pickedWorkers;
        commandCenter.extractionUIRecons.text = "Recons:\t" + commandCenter.reconsInsideExtraction.ToString() + " / " + GameDataManager.Instance.pickedRecons;
        commandCenter.extractionUIGatherers.text = "Gatherers:\t" + commandCenter.gatherersInsideExtraction.ToString() + " / " + GameDataManager.Instance.pickedGatherers;
    }

    #endregion
}