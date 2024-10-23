using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class BuildingSystem : MonoBehaviour
{
    #region Variable
    public static BuildingSystem current;

    public GridLayout gridLayout;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private TileBase whiteTile;

    public GameObject TestBuilding;

    private PlacableObject objectToPlace;

    InputActions inputActions;
    #endregion
    #region Unity Functions

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
        inputActions = new InputActions();
        inputActions.Keyboard.Enable();
    }

    private void Update()
    {
        if (inputActions.Keyboard.InitializeBuilding.IsPressed())
        {
            InitializeObject(TestBuilding);
        }
    }
    #endregion
    #region Utils
    public static Vector3 GetMouseWorldPosition()
    {
         Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit raycastHit)){
            Debug.Log(raycastHit.point);
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    #endregion
    #region Building PLacement

    public void InitializeObject(GameObject prefab) {
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefab, position, Quaternion.identity);
        objectToPlace = obj.GetComponent<PlacableObject>();
        obj.AddComponent<ObjectDrag>();
}
    #endregion

}
