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
}
