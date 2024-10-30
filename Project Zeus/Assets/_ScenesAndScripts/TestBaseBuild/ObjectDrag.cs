using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDrag : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler 
{
    private Vector3 offset;
    private bool dragging = false;
    InputActions inputActions;
    BuildingSystem buildingSystem;

    private void Awake()
    {
        inputActions = new InputActions();
        inputActions.Mouse.Enable();
        buildingSystem = GameObject.FindGameObjectWithTag("BuildSystem").GetComponent<BuildingSystem>();
        dragging = true;
        buildingSystem.objectToPlace = gameObject.GetComponent<PlacableObject>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //When the mouse is over the Object and left-click is pressed the buidling is set to be dragged
        dragging = false;
        // offset = transform.position - BuildingSystem.GetMouseWorldPosition();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        if (dragging)
        {
            // If the buidling is being dragged it is moved to the mouse-position and alwasy snapped to the next grid-spot
            Vector3 pos = BuildingSystem.GetMouseWorldPosition(); //+ offset;
            transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
        }
    }
}
