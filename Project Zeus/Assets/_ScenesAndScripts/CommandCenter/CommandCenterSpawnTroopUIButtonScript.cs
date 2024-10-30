using System.Collections;
using TMPro;
using UnityEngine;

public class CommandCenterSpawnTroopUIButtonScript : MonoBehaviour
{

    [SerializeField] GameObject unitPrefab;
    [SerializeField] TextMeshProUGUI spawningTimerText;
    
    



    public void OnClick()
    {
        StartCoroutine(WaitForSeconds());
        StartCoroutine(StartCountdown());
        
        

    }


    IEnumerator WaitForSeconds()
    {
        Vector3 spawnPosition = new Vector3(21, 2.8f, 41);
        Quaternion spawnRotation = Quaternion.identity;

        yield return new WaitForSeconds(5);
        Instantiate(unitPrefab, spawnPosition, spawnRotation);
    }

    IEnumerator StartCountdown()
    {
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
    }
}

