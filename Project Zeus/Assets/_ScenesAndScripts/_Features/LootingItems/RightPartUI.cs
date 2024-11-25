using TMPro;
using UnityEngine;

public class RightPartUI : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI unitDescription;
    int unitInstances;

    void Start()
    {
        unitInstances = GameDataManager.Instance.pickedWorkers + GameDataManager.Instance.pickedRecons + GameDataManager.Instance.pickedGatherers;
        
        for (int i = 0; i < unitInstances; i++)
        {
            Instantiate(unitDescription, gameObject.transform);
        }
    }

   
    void Update()
    {
        
    }
}
