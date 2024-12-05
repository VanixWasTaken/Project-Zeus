using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class DeathScreenSceneHandler : MonoBehaviour
{
    #region References

    // references for the cutscene
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject returnCutscene;

    // references for the lost units display
    [SerializeField] TextMeshProUGUI workersText;
    [SerializeField] TextMeshProUGUI reconsText;
    [SerializeField] TextMeshProUGUI fightersText;

    #endregion

    #region Variables

    // variables to calculate the lost units
    int lostWorkers;
    int lostRecons;
    int lostFighters;

    int[] unitCount;

    #endregion



    #region Unity Built-In

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }

        SetText();
    }

    #region Buttons

    public void OnRTSButtonClicked()
    {
        returnCutscene.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit(); // does nothing in the editor,  but quits the game when built
    }

    #endregion

    #region Video Player

    private void OnVideoEnd(VideoPlayer videoPlayer)
    {
        GameDataManager.Instance.currentKilogram = 0;
        GameDataManager.Instance.pickedWorkers = 0;
        GameDataManager.Instance.pickedRecons = 0;
        GameDataManager.Instance.pickedFighters = 0;
        SceneManager.LoadScene("DeployMenu");
    }

    #endregion

    #endregion

    #region Custom Functions()

    private void SetText()
    {
        ///<summary>
        /// In this function the script gets the values of every picked unit and stores it in an array
        /// 
        /// After that, it iterates through the array in a for loop and updates the text in the unit display
        ///</summary>

        unitCount = GameDataManager.Instance.GetLostUnitCount();

        for (int i = 0; i < unitCount.Length; i++)
        {
            if (i == 0)
            {
                lostWorkers = unitCount[i];
                workersText.text = ("Workers\t\t: " + lostWorkers);
            }
            else if (i == 1)
            {
                lostRecons = unitCount[i];
                reconsText.text = ("Recons\t\t: " + lostRecons);
            }
            else if (i == 2)
            {
                lostFighters = unitCount[i];
                fightersText.text = ("Gatherers\t\t: " + lostFighters);
            }
        }
    }

    private int[] GetLostUnits()
    {
        return unitCount;
    }

    private void OnDestroy()
    {
        GameDataManager.Instance.UpdateAvailableUnits(GetLostUnits());
    }

    #endregion
}