using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class DeathScreenSceneHandler : MonoBehaviour
{
    // references for the cutscene
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject returnCutscene;

    // references for the lost units display
    [SerializeField] TextMeshProUGUI workersText;
    [SerializeField] TextMeshProUGUI reconsText;
    [SerializeField] TextMeshProUGUI gatherersText;

    // variables to calculate the lost units
    int lostWorkers;
    int lostRecons;
    int lostGatherers;

    int[] unitCount;


    void Awake()
    {
        if (returnCutscene == null)
        {
            returnCutscene = GameObject.Find("ReturnCutscene");
        }

        if (videoPlayer == null)
        {
            videoPlayer = returnCutscene.GetComponentInChildren<VideoPlayer>();
        }

        
    }

    void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }

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
                lostGatherers = unitCount[i];
                gatherersText.text = ("Gatherers\t\t: " + lostGatherers);
            }
        }
    }


    // public methods
    public void OnRTSButtonClicked()
    {
        returnCutscene.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit(); // does nothing in the editor,  but quits the game when built
    }

    // private methods
    void OnVideoEnd(VideoPlayer videoPlayer)
    {
        GameDataManager.Instance.currentKilogram = 0;
        GameDataManager.Instance.pickedWorkers = 0;
        GameDataManager.Instance.pickedRecons = 0;
        GameDataManager.Instance.pickedGatherers = 0;
        SceneManager.LoadScene("DeployMenu");
    }

    int[] GetLostUnits()
    {
        return unitCount;
    }

    void OnDestroy()
    {
        GameDataManager.Instance.UpdateAvailableUnits(GetLostUnits());
    }
}
