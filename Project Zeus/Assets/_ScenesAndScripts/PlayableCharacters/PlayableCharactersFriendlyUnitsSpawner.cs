using Unity.VisualScripting;
using UnityEngine;

public class PlayableCharactersFriendlyUnitsSpawner : MonoBehaviour
{

    [SerializeField] GameObject workerPrefab;
    [SerializeField] GameObject reconPrefab;
    [SerializeField] GameObject gathererPrefab;
    [SerializeField] GameObject friendlyUnitsGO;

    float spawnAreaWidth = 5;

    void Start()
    {
        // Initializing the units that was chosen in the DeployMenu
        for (int i = 0; i < GameDataManager.Instance.pickedWorkers; i++)
        {
            // Determines a random position around the predetermined spawnpoint where each individual unit should spawn
            float xOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            float zOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            Vector3 spawnPosition = friendlyUnitsGO.transform.position + new Vector3(xOffset, 0, zOffset);
            Instantiate(workerPrefab, spawnPosition, Quaternion.identity);
        }
        for (int i = 0; i < GameDataManager.Instance.pickedGatherers; i++)
        {
            // Determines a random position around the predetermined spawnpoint where each individual unit should spawn
            float xOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            float zOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            Vector3 spawnPosition = friendlyUnitsGO.transform.position + new Vector3(xOffset, 0, zOffset);
            Instantiate(gathererPrefab, spawnPosition, Quaternion.identity);
        }
        for (int i = 0; i < GameDataManager.Instance.pickedRecons; i++)
        {
            // Determines a random position around the predetermined spawnpoint where each individual unit should spawn
            float xOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            float zOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            Vector3 spawnPosition = friendlyUnitsGO.transform.position + new Vector3(xOffset, 0, zOffset);
            Instantiate(reconPrefab, spawnPosition, Quaternion.identity);
        }
    }


    void Update()
    {
        
    }
}
