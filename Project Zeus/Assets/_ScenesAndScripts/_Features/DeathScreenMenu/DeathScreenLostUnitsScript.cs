using TMPro;
using UnityEngine;

public class DeathScreenLostUnitsScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI workersText;
    [SerializeField] TextMeshProUGUI reconsText;
    [SerializeField] TextMeshProUGUI gatherersText;

    int lostWorkers;
    int lostRecons;
    int lostGatherers;

    int[] unitCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        unitCount = GameDataManager.Instance.GetLostUnitCount();

        for (int i = 0; i < unitCount.Length; i++)
        {
            if (i == 0) 
            { 
                lostWorkers = unitCount[i];
                workersText.text = ("Workers\t\t: " + lostWorkers);
            }
            else if (i == 1) 
            {
                lostRecons = unitCount[i];
                reconsText.text = ("Recons\t\t: " + lostRecons);
            }
            else if (i == 2)
            {
                lostGatherers = unitCount[i];
                gatherersText.text = ("Gatherers\t\t: " + lostGatherers);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int[] GetLostUnits()
    {
        return unitCount;
    }

    private void OnDestroy()
    {
        GameDataManager.Instance.UpdateAvailableUnits(GetLostUnits());
    }
}
