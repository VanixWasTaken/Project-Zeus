using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class BuildingSystem : MonoBehaviour
{
    #region Variable
    public static BuildingSystem current;

    public GridLayout gridLayout;
    [SerializeField] private Grid grid;
    [SerializeField] private Tilemap tileMap;
    [SerializeField] private TileBase whiteTile;

    public GameObject TestBuilding;

    public PlacableObject objectToPlace;

    InputActions inputActions;
    #endregion

    #region Unity Functions

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
        inputActions = new InputActions();
        inputActions.Keyboard.Enable();
        inputActions.Mouse.Enable();
    }

    private void Update()
    {
        if (!objectToPlace)
        {
            return;
        }
 
        if (inputActions.Mouse.Released.WasReleasedThisFrame())
        {
            if (CanBePlaced(objectToPlace))
            {
                Debug.Log("Releasedthisfrm");
                objectToPlace.Place();
                Vector3Int start = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);
            }
            else
            {
                Destroy(objectToPlace.gameObject);
            }
        }
        else if (inputActions.Mouse.RightClick.IsPressed())
        {
            Destroy(objectToPlace.gameObject);
        }

    }
    #endregion

    #region Utils
    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Debug.Log(ray);
        if (Physics.Raycast(ray, out RaycastHit raycastHit)){
            return raycastHit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }
    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        //Checks the Buidlings position and checks for the nearest grid-spot.
        //Then it snaps the building to that Grid-Spot.
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        //Sets the size of the array to the size of the Tilemap area
        TileBase[] array = tilemap.GetTilesBlock(area);
        return array;
    }
    #endregion

    #region Building Placement

    public void InitializeObject(GameObject prefab)
    { 
        //Initializes a Building at the nearest Gridspot to the center of the world (0,0,0)
        Vector3 position = SnapCoordinateToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefab, GetMouseWorldPosition(), Quaternion.identity);
        //Gets the Placable Object Component for that Building to use that later, when using it 
       
    }

    private bool CanBePlaced(PlacableObject placeableObject)
    {
        //Checks if the Buidling can be placed at the current location by setting a small area the size of the building
        BoundsInt area = new BoundsInt();
        area.position = gridLayout.WorldToCell(objectToPlace.GetStartPosition());
        area.size = placeableObject.Size;
        area.size = new Vector3Int(area.size.x + 1, area.size.y +1 , area.size.z);

        TileBase[] baseArray = GetTilesBlock(area, tileMap);
        int i = 0;
        // Then it compares the tiles the building would be build on with the tiles that are in the ground-tilemap
        foreach (var b in baseArray)
        {
            i++;
            //If one of the Tiles you wan to place your building on is occupied by a white tile you cn't place the bulding there
            if (b == whiteTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        Debug.Log("Takeare");
        tileMap.BoxFill(start, whiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
        objectToPlace = null;
    }
    #endregion




    public void InstantiateObject()
    {
        InitializeObject(TestBuilding);
    }
}
