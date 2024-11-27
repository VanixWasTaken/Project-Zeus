using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    public int availableWorkers = 0;
    public int pickedWorkers;
    public int availableRecons = 0;
    public int pickedRecons;
    public int availableGatherers = 0;
    public int pickedGatherers;

    public float maxKilogram = 100;
    public float currentKilogram = 0;

    public int currentEnergy;
    public int maxEnergy = 100;

    public int collectedLoot= 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameDataManager instance set");
        }
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("Another instance of GameDataManager was destroyed");
        }
    }


    public void IncreaseCurrentKilogram(float value)
    {
        currentKilogram += value;
    }
    public void DecreaseCurrentKilogram(float value)
    {
        currentKilogram -= value;
    }

    public int[] GetLostUnitCount()
    {
        int[] unitValues = new int[3];

        for (int i = 0; i < unitValues.Length; i++)
        {
            int unitCount = 0;

            if (i == 0)
            {
                unitCount = pickedWorkers;
            }
            else if(i == 1) 
            {
                unitCount = pickedRecons;
            }
            else if (i == 2)
            {
                unitCount = pickedGatherers;
            }

            unitValues[i] = unitCount;
            Debug.Log(unitValues[i]);
        }

        return unitValues;
    }

    public void UpdateAvailableUnits(int[] lostUnits)
    {
        for (int i = 0; i < lostUnits.Length; i++)
        {

            if (i == 0 && availableWorkers > 0)
            {
                availableWorkers -= lostUnits[i];
            }
            else if (i == 1 && availableRecons > 0)
            {
                availableRecons -= lostUnits[i];
            }
            else if (i == 2 && availableGatherers > 0)
            {
                availableGatherers -= lostUnits[i];
            }
        }
    }
}
