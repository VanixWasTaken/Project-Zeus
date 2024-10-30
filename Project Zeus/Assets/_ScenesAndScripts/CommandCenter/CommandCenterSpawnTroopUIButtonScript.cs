using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Audio.AudioType;

public class CommandCenterSpawnTroopUIButtonScript : MonoBehaviour
{

    public GameObject unitPrefab;
    public AudioController audioController;


    private void Start()
    {
        audioController = FindAnyObjectByType<AudioController>();
    }

    public void OnClick()
    {
        audioController.PlayAudio(HeadquarterDroneSpawn_01);

        Vector3 spawnPosition = new Vector3(0, 0, 0); 
        Quaternion spawnRotation = Quaternion.identity;

        Instantiate(unitPrefab, spawnPosition, spawnRotation);
    }
}
