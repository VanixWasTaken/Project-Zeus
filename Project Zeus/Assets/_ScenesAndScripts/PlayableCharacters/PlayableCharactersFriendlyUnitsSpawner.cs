using UnityEngine;

public class PlayableCharactersFriendlyUnitsSpawner : MonoBehaviour
{

    [SerializeField] GameObject workerPrefab;
    [SerializeField] GameObject friendlyUnitsGO;

    void Start()
    {
      Instantiate(workerPrefab, friendlyUnitsGO.transform);
    }


    void Update()
    {
        
    }
}
