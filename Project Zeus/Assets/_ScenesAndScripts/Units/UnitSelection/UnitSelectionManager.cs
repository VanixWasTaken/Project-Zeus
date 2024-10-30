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

    // Selection box UI
    public RectTransform selectionBox; // Drag your UI Image here for the selection box
    Vector2 startPosition;
    Vector2 endPosition;

    public AudioController audioController;

    List<UnitStateManager> selectedUnits = new List<UnitStateManager>();




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

        foreach (UnitStateManager unit in FindObjectsByType<UnitStateManager>(FindObjectsSortMode.None))
        {
            Vector3 screenPosition = mainCamera.WorldToScreenPoint(unit.transform.position);

            if (screenPosition.x >= min.x && screenPosition.x <= max.x &&
                screenPosition.y >= min.y && screenPosition.y <= max.y)
            {
                unit.Select();
                selectedUnits.Add(unit);  // Only add the unit to the selection
            }
        }

        if (selectedUnits.Count > 0) 
        {
            audioController.PlayAudio(SMSelectedBark_01);
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

                audioController.PlayAudio(DroneAffirmBark_01);

                MoveUnitsToTargetWithSpacing(targetPosition);
            }
        }
    }

    void MoveUnitsToTargetWithSpacing(Vector3 targetPosition)
    {
        float spacing = 1.5f; // Set a spacing distance

        foreach (UnitStateManager unit in selectedUnits)
        {
            // Calculate a new position based on the current unit's position and desired target
            Vector3 offset = new Vector3(Random.Range(-spacing, spacing), 0, Random.Range(-spacing, spacing));
            Vector3 adjustedTarget = targetPosition + offset;

            
            unit.OnCommandMove(adjustedTarget);
        }
    }
}
