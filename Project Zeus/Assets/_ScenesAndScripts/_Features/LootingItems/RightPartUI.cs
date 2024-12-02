using TMPro;
using UnityEngine;

public class RightPartUI : MonoBehaviour
{

    #region References

    [SerializeField] TextMeshProUGUI unitDescription;

    #endregion

    #region Variables

    private int unitInstances;

    #endregion


    #region Unity Build In

    void Start()
    {
        unitInstances = GameDataManager.Instance.pickedWorkers + GameDataManager.Instance.pickedRecons + GameDataManager.Instance.pickedGatherers;
        
        for (int i = 0; i < unitInstances; i++)
        {
            Instantiate(unitDescription, gameObject.transform);
        }
    }

    #endregion
}
