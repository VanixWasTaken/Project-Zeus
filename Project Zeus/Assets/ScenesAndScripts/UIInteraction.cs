using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInteraction : MonoBehaviour
{

    public GameObject uiCanvas;
    GraphicRaycaster uiRaycaster;

    
    void Start()
    {
        
    }

   
    void Update()
    {
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            GetUIElementsClicked();
        }



    }


    void GetUIElementsClicked()
    {

    }
}
