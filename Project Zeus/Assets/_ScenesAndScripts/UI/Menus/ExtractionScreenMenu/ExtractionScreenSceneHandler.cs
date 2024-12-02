using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ExtractionScreenSceneHandler : MonoBehaviour
{
    #region References

    // references for the cutscene
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject returnCutscene;

    // references for the lost units display
    [SerializeField] TextMeshProUGUI workersText;
    [SerializeField] TextMeshProUGUI reconsText;
    [SerializeField] TextMeshProUGUI gatherersText;

    #endregion

    #region Variables

    // variables to display the units that return
    int savedWorkers;
    int savedRecons;
    int savedGatherers;

    int[] unitCount;

    #endregion



    #region Unity Built-In

    void Start()
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
        ResetGameData();
        SceneManager.LoadScene("DeployMenu");
    }

    #endregion

    #endregion

    #region Custom Functions()

    private void SetText()
    {
        unitCount = GameDataManager.Instance.GetSavedUnitCount();

        for (int i = 0; i < unitCount.Length; i++)
        {
            if (i == 0)
            {
                savedWorkers = unitCount[i];
                workersText.text = ("Workers\t\t: " + savedWorkers);
            }
            else if (i == 1)
            {
                savedRecons = unitCount[i];
                reconsText.text = ("Recons\t\t: " + savedRecons);
            }
            else if (i == 2)
            {
                savedGatherers = unitCount[i];
                gatherersText.text = ("Gatherers\t\t: " + savedGatherers);
            }
        }
    }

    

    int[] GetLostUnits()
    {
        int[] losses = new int[3];

        for (int i = 0; i < losses.Length; i++)
        {
            if (i == 0)
            {
                losses[i] = GameDataManager.Instance.pickedWorkers - savedWorkers;
            }
            else if (i == 1)
            {
                losses[i] = GameDataManager.Instance.pickedRecons - savedRecons;
            }
            else if (i == 2)
            {
                losses[i] = GameDataManager.Instance.pickedGatherers - savedGatherers;
            }

            Debug.Log(losses[i]);
        }

        return losses;
    }

    private void ResetGameData()
    {
        GameDataManager.Instance.UpdateAvailableUnits(GetLostUnits());
        GameDataManager.Instance.currentKilogram = 0;
        GameDataManager.Instance.extractedWorkers = 0;
        GameDataManager.Instance.extractedRecons = 0;
        GameDataManager.Instance.extractedGatherers = 0;
        GameDataManager.Instance.pickedWorkers = 0;
        GameDataManager.Instance.pickedRecons = 0;
        GameDataManager.Instance.pickedGatherers = 0;
    }

    #endregion
    
}
