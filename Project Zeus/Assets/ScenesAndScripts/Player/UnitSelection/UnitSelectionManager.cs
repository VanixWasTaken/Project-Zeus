using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UnitSelectionManager : MonoBehaviour
{
    private InputActions inputActions;
    public Camera mainCamera;
    public LayerMask unitLayer;

    // Selection box UI
    public RectTransform selectionBox; // Drag your UI Image here for the selection box
    private Vector2 startPosition;
    private Vector2 endPosition;

    private List<UnitStateManager> selectedUnits = new List<UnitStateManager>();

    private void Start()
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

    private void HandleSelection()
    {
        // Handle click selection and drag start
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // Reset the selection box size to zero to avoid flashing
            selectionBox.sizeDelta = Vector2.zero;

            startPosition = Mouse.current.position.ReadValue();
            selectionBox.gameObject.SetActive(true);
        }
        // Update the selection box while dragging
        else if (Mouse.current.leftButton.isPressed)
        {
            endPosition = Mouse.current.position.ReadValue();
            UpdateSelectionBox();
        }
        // Release the selection box and select units within it
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            SelectUnitsInBox();
            selectionBox.gameObject.SetActive(false);
        }
    }

    private void UpdateSelectionBox()
    {
        Vector2 boxStart = startPosition;
        Vector2 boxEnd = endPosition;

        Vector2 boxSize = boxEnd - boxStart;
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(boxSize.x), Mathf.Abs(boxSize.y));
        selectionBox.anchoredPosition = boxStart + boxSize / 2;
    }

    private void SelectUnitsInBox()
    {
        // Get positions of selected units
        Vector2 min = startPosition;
        Vector2 max = endPosition;

        // Ensure min is bottom-left and max is top-right
        if (min.x > max.x) (min.x, max.x) = (max.x, min.x);
        if (min.y > max.y) (min.y, max.y) = (max.y, min.y);

        // Deselect all previously selected units
        foreach (var unit in selectedUnits)
        {
            unit.Deselect();
        }
        selectedUnits.Clear();

        // Find all units within the selection box
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

        // Move the selected units to a target position with spacing
        if (selectedUnits.Count > 0)
        {
            MoveUnitsToTargetWithSpacing(endPosition); // Use the end position as the target
        }
    }

    private void MoveUnitsToTargetWithSpacing(Vector3 targetPosition)
    {
        float spacing = 1.5f; // Set a spacing distance

        foreach (UnitStateManager unit in selectedUnits)
        {
            // Calculate a new position based on the target position and random offset for spacing
            Vector3 offset = new Vector3(Random.Range(-spacing, spacing), 0, Random.Range(-spacing, spacing));
            Vector3 adjustedTarget = targetPosition + offset;

            unit.OnCommandMove(adjustedTarget);
        }
    }

    private void HandleCommands()
    {
        // Handle movement command with right-click
        if (Mouse.current.rightButton.wasPressedThisFrame && selectedUnits.Count > 0)
        {
            Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                MoveUnitsToTargetWithSpacing(hit.point); // Move units to the clicked point with spacing
            }
        }
    }
}
