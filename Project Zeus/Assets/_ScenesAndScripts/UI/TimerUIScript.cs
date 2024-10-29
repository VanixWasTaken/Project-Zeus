using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIScript : MonoBehaviour
{

    TextMeshProUGUI timerText;
    float timeElapsed = 0f;



    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        UpdateTimerDisplay();
    }


    // Updates the visual timer and shows it in the correct form, for example -->  12:51
    void UpdateTimerDisplay()
    {
        float minutes = Mathf.FloorToInt(timeElapsed / 60);
        float seconds = Mathf.FloorToInt(timeElapsed % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
