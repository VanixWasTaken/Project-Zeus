using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using static UnityEngine.Audio.AudioType;

public class CommandCenterSpawnTroopUIButtonScript : MonoBehaviour
{

    private AudioController audioController;
    [SerializeField] GameObject unitPrefab;
    [SerializeField] TextMeshProUGUI spawningTimerText;
    [SerializeField] Material redSpaceMarine;

    private void Start()
    {
        audioController = FindAnyObjectByType<AudioController>();
    }

    public void OnClick()
    {
        // Start the timer coroutine
        StartCoroutine(StartCountdown());
        StartCoroutine(WaitForSeconds());
    }

    

    private void Update()
    {
        // No need to do anything in Update for now
    }

    IEnumerator StartCountdown()
    {
        int countdownTime = 6; // Timer starting value
        while (countdownTime > 0)
        {
            spawningTimerText.text = countdownTime.ToString(); // Update text
            yield return new WaitForSeconds(1); // Wait for 1 second
            countdownTime--; // Decrement the timer
        }

        // After the countdown ends, reset the text and the timer
        spawningTimerText.text = "0"; // Set to zero when countdown ends
        // Optional: Wait a moment before resetting (if needed)
        yield return new WaitForSeconds(1);
        spawningTimerText.text = "6"; // Reset timer display for next round
    }

    IEnumerator WaitForSeconds()
    {
        Vector3 spawnPosition = new Vector3(98, 10, 3);
        Quaternion spawnRotation = Quaternion.identity;
        audioController.PlayAudio(HeadquarterDroneSpawn_01);

        yield return new WaitForSeconds(5);
        GameObject obj = Instantiate(unitPrefab, spawnPosition, spawnRotation);
        obj.GetComponent<UnitStateManager>().myEnemyTag = "Enemy";
        obj.GetComponentInChildren<SkinnedMeshRenderer>().material = redSpaceMarine;
        obj.transform.SetParent(GameObject.FindGameObjectWithTag("PlayerContainer").transform);
    }
}
