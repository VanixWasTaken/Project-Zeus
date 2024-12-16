using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    #region Variables

    [Header("Available Units")]
    public int availableWorkers = 0;
    public int availableRecons = 0;
    public int availableFighters = 0;
    public int availableMineralQuenchers = 0;

    [Header("Picked Units")]
    public int pickedWorkers;
    public int pickedRecons;
    public int pickedFighters;
    public int pickedMineralQuenchers;

    [Header("Extracted Units")]
    public int extractedWorkers;
    public int extractedRecons;
    public int extractedFighters;
    public int extractedMineralQuenchers;

    [Header("Weight System")]
    public float maxKilogram = 100;
    public float currentKilogram = 0;

    [Header("Energy System")]
    public int currentEnergy;
    public int maxEnergy = 100;

    [Header("Loot System")]
    public int collectedLoot= 0;

    #endregion


    #region Unity Build In

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

    #endregion



    #region Custom Function()

    public void IncreaseCurrentKilogram(float value)
    {
        currentKilogram += value;
    }

    public void DecreaseCurrentKilogram(float value)
    {
        currentKilogram -= value;
    }

    public int[] GetSavedUnitCount()
    {
        int[] unitValues = new int[3];

        for (int i = 0; i < unitValues.Length; i++)
        {
            int unitCount = 0;

            if (i == 0)
            {
                unitCount = extractedWorkers;
            }
            else if (i == 1)
            {
                unitCount = extractedRecons;
            }
            else if (i == 2)
            {
                unitCount = extractedFighters;
            }

            unitValues[i] = unitCount;
        }

        return unitValues;
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
            else if (i == 1)
            {
                unitCount = pickedRecons;
            }
            else if (i == 2)
            {
                unitCount = pickedFighters;
            }

            unitValues[i] = unitCount;
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
            else if (i == 2 && availableFighters > 0)
            {
                availableFighters -= lostUnits[i];
            }
        }
    }

    #endregion
}
