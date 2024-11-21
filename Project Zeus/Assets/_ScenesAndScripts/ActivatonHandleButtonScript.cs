using UnityEngine;
using System.Collections;

public class ActivationHandleButtonScript : MonoBehaviour
{
    UnitSelectionManager unitSelectionManager;

    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("SelectionManager");
        foreach (GameObject obj in objects) 
        { 
            if (obj != null)
            {
                if (obj.name == "UnitSelectionManager")
                {
                    unitSelectionManager = obj.GetComponent<UnitSelectionManager>();
                }
            }
        }
    }

    public void OnDeactivateButtonClicked()
    {
        unitSelectionManager.ShutDownSelected();
    }

    public void OnActivateButtonClicked()
    {
        unitSelectionManager.ActivateSelected();
    }
}
