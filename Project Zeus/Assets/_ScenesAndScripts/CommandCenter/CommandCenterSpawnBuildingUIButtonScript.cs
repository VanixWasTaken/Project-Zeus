using System.Collections;
using TMPro;
using UnityEngine;

public class CommandCenterSpawnBuildingUIButtonScript : MonoBehaviour
{

    [SerializeField] GameObject buildingPrefab;
    [SerializeField] TextMeshProUGUI spawningTimerText;
    bool isAllowedToSpawnUnit = true;

    [SerializeField] BuildingSystem buildingSystem;



    public void OnClick()
    {
        if (isAllowedToSpawnUnit)
        {
            StartCoroutine(WaitForSeconds());
            StartCoroutine(StartCountdown());
        }
    }


    IEnumerator WaitForSeconds()
    {
        Vector3 spawnPosition = new Vector3(21, 2.8f, 41);
        Quaternion spawnRotation = Quaternion.identity;

        yield return new WaitForSeconds(1);
        buildingSystem.InstantiateObject();
    }

    IEnumerator StartCountdown()
    {
        isAllowedToSpawnUnit = false;
        int countdownTime = 5; // Timer starting value
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
        spawningTimerText.text = "5"; // Reset timer display for next round
        isAllowedToSpawnUnit = true;
    }
}
