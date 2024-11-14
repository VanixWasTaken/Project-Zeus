using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Audio.AudioType;
using UnityEngine.InputSystem;

public class UnitSelectionManager : MonoBehaviour
{
    InputActions inputActions;
    public Camera mainCamera;
    public LayerMask unitLayer;

    
    #region Selection Box UI
    public RectTransform selectionBox; // Drag your UI Image here for the selection box
    Vector2 startPosition;
    Vector2 endPosition;
    #endregion

    List<WorkerStateManager> selectedUnits = new List<WorkerStateManager>();



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
            SelectUnitsInBox();
            selectionBox.gameObject.SetActive(false);
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
        selectedUnits.Clear();

        foreach (WorkerStateManager unit in FindObjectsByType<WorkerStateManager>(FindObjectsSortMode.None))
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(unit.transform.position);

            if (screenPosition.x >= min.x && screenPosition.x <= max.x &&
                screenPosition.y >= min.y && screenPosition.y <= max.y)
            {
                unit.Select();
                if(unit.gameObject.tag != "Enemy")
                {
                    selectedUnits.Add(unit);
                }
                 // Only add the unit to the selection
            }
        }

    }

    void HandleCommands()
    {
        // Handle movement command with right-click
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

        foreach (WorkerStateManager unit in selectedUnits)
        {
            // Calculate a new position based on the current unit's position and desired target
            Vector3 offset = new Vector3(Random.Range(-spacing, spacing), 0, Random.Range(-spacing, spacing));
            Vector3 adjustedTarget = targetPosition + offset;

            
            unit.OnCommandMove(adjustedTarget);
        }
    }
}
