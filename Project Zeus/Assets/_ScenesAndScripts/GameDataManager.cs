using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    public int availableWorkers = 2;
    public int availableRecons = 10;
    public int availableGatherers = 8;


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


}
