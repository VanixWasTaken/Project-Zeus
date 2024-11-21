using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerUIScript : MonoBehaviour
{

    TextMeshProUGUI timerText;
    [SerializeField] GameObject swarmWarning;
    float timeElapsed = 0f;
    bool warningFlashing = false;

    [SerializeField] float warningTime;
    [SerializeField] float endTime;

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

        if (minutes >= warningTime && seconds <= 1)
        {
            if (warningFlashing == false)
            {
                StartCoroutine(FlashWarning());
            }
        }
        else if (minutes >= endTime)
        {
            StopAllCoroutines();
            SceneManager.LoadScene("DeathScreenMenu");
        }
    }

    IEnumerator FlashWarning()
    {
        warningFlashing = true;
        while (true)
        {
            swarmWarning.SetActive(true);
            yield return new WaitForSeconds(1);
            swarmWarning.SetActive(false);
            yield return new WaitForSeconds(1);
        }
    }
}
