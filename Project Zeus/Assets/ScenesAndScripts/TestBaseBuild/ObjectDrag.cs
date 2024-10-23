using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ObjectDrag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 offset;
    private bool dragging = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = transform.position - BuildingSystem.GetMouseWorldPosition();
        dragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        dragging = false;
    }

    private void Update()
    {
        if (dragging)
        {
            Vector3 pos = BuildingSystem.GetMouseWorldPosition() + offset;
            transform.position = BuildingSystem.current.SnapCoordinateToGrid(pos);
        }
        
    }
}
