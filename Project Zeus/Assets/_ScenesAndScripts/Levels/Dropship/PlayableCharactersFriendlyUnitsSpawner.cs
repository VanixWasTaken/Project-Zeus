using Unity.VisualScripting;
using UnityEngine;

public class PlayableCharactersFriendlyUnitsSpawner : MonoBehaviour
{

    #region References

    [SerializeField] GameObject workerPrefab;
    [SerializeField] GameObject reconPrefab;
    [SerializeField] GameObject fighterPrefab;
    [SerializeField] GameObject friendlyUnitsGO;

    #endregion

    #region Variables

    private float spawnAreaWidth = 5;

    #endregion


    #region Unity Build In

    private void Start()
    {
        // Initializing the units that was chosen in the DeployMenu
        for (int i = 0; i < GameDataManager.Instance.pickedWorkers; i++)
        {
            // Determines a random position around the predetermined spawnpoint where each individual unit should spawn
            float xOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            float zOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            Vector3 spawnPosition = friendlyUnitsGO.transform.position + new Vector3(xOffset, 0, zOffset);
            Instantiate(workerPrefab, spawnPosition, Quaternion.identity, friendlyUnitsGO.transform);
        }
        for (int i = 0; i < GameDataManager.Instance.pickedFighters; i++)
        {
            // Determines a random position around the predetermined spawnpoint where each individual unit should spawn
            float xOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            float zOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            Vector3 spawnPosition = friendlyUnitsGO.transform.position + new Vector3(xOffset, 0, zOffset);
            Instantiate(fighterPrefab, spawnPosition, Quaternion.identity, friendlyUnitsGO.transform);
        }
        for (int i = 0; i < GameDataManager.Instance.pickedRecons; i++)
        {
            // Determines a random position around the predetermined spawnpoint where each individual unit should spawn
            float xOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            float zOffset = Random.Range(-spawnAreaWidth, spawnAreaWidth);
            Vector3 spawnPosition = friendlyUnitsGO.transform.position + new Vector3(xOffset, 0, zOffset);
            Instantiate(reconPrefab, spawnPosition, Quaternion.identity, friendlyUnitsGO.transform);
        }
    }

    #endregion
}
