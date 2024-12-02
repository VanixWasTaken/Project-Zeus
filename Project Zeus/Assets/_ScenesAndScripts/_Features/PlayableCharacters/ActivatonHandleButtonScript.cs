using UnityEngine;

public class ActivationHandleButtonScript : MonoBehaviour
{
    #region References

    UnitSelectionManager unitSelectionManager;

    #endregion



    #region Unity Built-In

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

    #region Buttons

    public void OnDeactivateButtonClicked()
    {
        unitSelectionManager.ShutDownSelected();
    }

    public void OnActivateButtonClicked()
    {
        unitSelectionManager.ActivateSelected();
    }

    #endregion

    #endregion
}
