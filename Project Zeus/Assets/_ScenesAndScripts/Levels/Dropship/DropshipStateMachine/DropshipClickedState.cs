using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class DropshipClickedState : DropshipBaseState
{

    #region References

    private InputActions inputActions;

    #endregion

    #region Variables

    private bool aboveButton = false;

    #endregion


    #region Unity Build In

    public override void EnterState(DropshipStateManager dropship)
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();

        dropship.extracttionUnitInfo.gameObject.SetActive(true);
    }

    public override void UpdateState(DropshipStateManager dropship)
    {
        HoversAboveButton(dropship);

        ClickedOnBase(dropship);

        UpdateText(dropship);
    }

    #endregion



    #region Custom Function()

    private void HoversAboveButton(DropshipStateManager dropship) // Checks If the mouse currently is above a Button
    {
        // Create PointerEventData
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

        // Raycast to detect UI elements
        List<RaycastResult> results = new List<RaycastResult>();
        GraphicRaycaster raycaster = dropship.extractionUIExtractButton.GetComponentInParent<Canvas>().GetComponent<GraphicRaycaster>();
        raycaster.Raycast(pointerEventData, results);

        // Check if the button is in the results
        aboveButton = results.Exists(result => result.gameObject == dropship.extractionUIExtractButton.gameObject || dropship.extractionWarningMenu.gameObject);
    }

    private void ClickedOnBase(DropshipStateManager dropship)
    {
        if (!dropship.hoversAbove)
        {
            if (Mouse.current.leftButton.isPressed && !aboveButton)
            {
                dropship.extracttionUnitInfo.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateText(DropshipStateManager dropship)
    {
        dropship.extractionUIWorkers.text = "Workers:\t" + dropship.workersInsideExtraction.ToString() + " / " + GameDataManager.Instance.pickedWorkers;
        dropship.extractionUIRecons.text = "Recons:\t" + dropship.reconsInsideExtraction.ToString() + " / " + GameDataManager.Instance.pickedRecons;
        dropship.extractionUIFighters.text = "Fighters:\t" + dropship.fightersInsideExtraction.ToString() + " / " + GameDataManager.Instance.pickedFighters;
    }

    #endregion
}