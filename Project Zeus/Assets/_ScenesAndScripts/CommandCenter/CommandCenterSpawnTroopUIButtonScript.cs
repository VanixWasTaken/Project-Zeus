using UnityEngine;

public class CommandCenterSpawnTroopUIButtonScript : MonoBehaviour
{

    public GameObject unitPrefab;



    void OnClick()
    {
        Vector3 spawnPosition = new Vector3(0, 0, 0); 
        Quaternion spawnRotation = Quaternion.identity;

        Instantiate(unitPrefab, spawnPosition, spawnRotation);
    }
}
