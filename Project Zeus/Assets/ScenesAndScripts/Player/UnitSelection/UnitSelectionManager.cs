using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionManager : MonoBehaviour
{
    InputActions inputActions;

    public Camera mainCamera;
    public LayerMask unitLayer;
    private List<UnitStateManager> selectedUnits = new List<UnitStateManager>();

    private void Start()
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();
    }

    void Update()
    {
        HandleSelection();
        HandleCommands();
    }

    private void HandleSelection()
    {
        if (inputActions.Mouse.Click.triggered == true)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, unitLayer))
            {
                Debug.Log("Raycast hit: " + hit.collider.gameObject.name); // Check what object was hit

                UnitStateManager unit = hit.collider.GetComponent<UnitStateManager>();
                if (unit != null)
                {
                    Debug.Log("Unit selected: " + unit.name); // Confirm unit is selected
                    if (!selectedUnits.Contains(unit))
                    {
                        unit.Select();
                        selectedUnits.Add(unit);
                    }
                    else
                    {
                        unit.Deselect();
                        selectedUnits.Remove(unit);
                    }
                }
            }
            else
            {
                Debug.Log("Raycast did not hit a unit");
            }
        }
    }

    private void HandleCommands()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && selectedUnits.Count > 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                foreach (UnitStateManager unit in selectedUnits)
                {
                    unit.OnCommandMove(hit.point);
                }
            }
        }
    }
}
