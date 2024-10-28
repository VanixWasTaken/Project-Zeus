using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{

    public GameObject uiCanvas;
    GraphicRaycaster uiRaycaster;

    PointerEventData clickData;
    List<RaycastResult> clickResult;

    
    void Start()
    {
        uiRaycaster = uiCanvas.GetComponent<GraphicRaycaster>();
        clickData = new PointerEventData(EventSystem.current);
        clickResult = new List<RaycastResult>();
    }

   
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GetUiElementsClicked();
        }
    }


    void GetUiElementsClicked()
    {
        clickData.position = Mouse.current.position.ReadValue();
        clickResult.Clear();

        uiRaycaster.Raycast(clickData, clickResult);

        foreach (RaycastResult result in clickResult)
        {
            GameObject uiElement = result.gameObject;

            //Debug.Log(uiElement.name);
            if (uiElement.name == "CommandCenterSpawnTrooperUI")
            {
                Debug.Log("asdasd");
            }
        } 
    }
}
