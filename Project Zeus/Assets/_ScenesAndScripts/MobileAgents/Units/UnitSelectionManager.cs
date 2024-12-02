using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelectionManager : MonoBehaviour
{

    #region References

    public Camera mainCamera;
    public RectTransform selectionBox; // Drag your UI Image here for the selection box
    [SerializeField] GameObject activationButtons;
    private InputActions inputActions;

    private List<UnitStateManager> selectedUnits = new List<UnitStateManager>();
    private List<UnitStateManager> lastSelectedUnits = new List<UnitStateManager>();

    #endregion

    #region Variables

    public LayerMask unitLayer;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private bool keepSelected;

    #endregion


    #region Unity Build In

    void Start()
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();

        selectionBox.gameObject.SetActive(false); // Hide the selection box initially
    }

    void Update()
    {
        HandleSelection();
        HandleCommands();
    }

    #endregion



    #region Custom Functions()

    void HandleSelection()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // Handle click selection and drag start
        {
            // Reset the selection box size to zero to avoid flashing
            selectionBox.sizeDelta = Vector2.zero;
            

            startPosition = Mouse.current.position.ReadValue();
            selectionBox.gameObject.SetActive(true);
        }
        else if (Mouse.current.leftButton.isPressed) // Update the selection box while dragging
        {
            endPosition = Mouse.current.position.ReadValue();
            UpdateSelectionBox();
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame) // Release the selection box and select units within it
        {
            if (ActivationHandling() == false)
            {
                activationButtons.SetActive(false);
                SelectUnitsInBox();
                selectionBox.gameObject.SetActive(false);
            }
            else
            {
                KeepSelection();
            }
        }
    }

    void UpdateSelectionBox()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxSize = boxEnd - boxStart;
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(boxSize.x), Mathf.Abs(boxSize.y));
        selectionBox.anchoredPosition = boxStart + boxSize / 2;
    }

    void SelectUnitsInBox()
    {
        Vector2 min = startPosition;
        Vector2 max = endPosition;

        if (min.x > max.x) (min.x, max.x) = (max.x, min.x);
        if (min.y > max.y) (min.y, max.y) = (max.y, min.y);

        foreach (var unit in selectedUnits)
        {
            unit.Deselect();
        }
        lastSelectedUnits = selectedUnits;
        selectedUnits.Clear();

        foreach (UnitStateManager unit in FindObjectsByType<UnitStateManager>(FindObjectsSortMode.None))
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(unit.transform.position);

            if (screenPosition.x >= min.x && screenPosition.x <= max.x &&
                screenPosition.y >= min.y && screenPosition.y <= max.y)
            {
                unit.Select();
                selectedUnits.Add(unit);
            }
        }

        if (selectedUnits.Count > 0)
        {
            activationButtons.SetActive(true);
        }
    }

    void HandleCommands() // Handle movement command with right-click
    {
        if (Mouse.current.rightButton.wasPressedThisFrame && selectedUnits.Count > 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 targetPosition = hit.point; // Get the target position from the raycast hit                


                MoveUnitsToTargetWithSpacing(targetPosition);
            }
        }
    }

    void MoveUnitsToTargetWithSpacing(Vector3 targetPosition)
    {
        float spacing = 2f; // Set a spacing distance

        foreach (UnitStateManager unit in selectedUnits)
        {
            // Calculate a new position based on the current unit's position and desired target
            Vector3 offset = new Vector3(Random.Range(-spacing, spacing), 0, Random.Range(-spacing, spacing));
            Vector3 adjustedTarget = targetPosition + offset;

            
            unit.OnCommandMove(adjustedTarget);
        }
    }

    bool ActivationHandling()
    {
        return keepSelected;
    }

    void KeepSelection()
    {
        foreach (UnitStateManager unit in lastSelectedUnits)
        {
            unit.Select();
        }
        selectedUnits = lastSelectedUnits;
        keepSelected = false;
    }

    public void RemoveDestroyedUnits(UnitStateManager _unit)
    {
        if (lastSelectedUnits.Contains(_unit))
        {
            lastSelectedUnits.Remove(_unit);
        }
        if (selectedUnits.Contains(_unit))
        {
            selectedUnits.Remove(_unit);
        }
    }

    public void ShutDownSelected()
    {
        if (lastSelectedUnits.Count > 0)
        {
            foreach (UnitStateManager unit in lastSelectedUnits)
            {
                unit.EnergyLogic("Deactivate");
            }
            keepSelected = true;
        }
    }

    public void ActivateSelected()
    {
        if (lastSelectedUnits.Count > 0)
        {
            foreach (UnitStateManager unit in lastSelectedUnits)
            {
                unit.EnergyLogic("Reactivate");
            }
            keepSelected = true;
        }
    }

    #endregion
}
