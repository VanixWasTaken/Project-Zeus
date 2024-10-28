using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIScript : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    float timeElapsed = 0f;

    
    void Update()
    {
        timeElapsed += Time.deltaTime;

        UpdateTimerDisplay();
    }



    void UpdateTimerDisplay()
    {
        float minutes = Mathf.FloorToInt(timeElapsed / 60);
        float seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
