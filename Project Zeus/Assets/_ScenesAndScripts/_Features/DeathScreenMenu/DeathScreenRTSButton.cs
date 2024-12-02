using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class DeathScreenRTSButton : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] GameObject returnCutscene;

    private void Start()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    public void OnRTSButtonClicked()
    {
        returnCutscene.SetActive(true);
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit(); // does nothing in the editor,  but quits the game when built
    }

    private void OnVideoEnd(VideoPlayer videoPlayer)
    {
        GameDataManager.Instance.currentKilogram = 0;
        GameDataManager.Instance.pickedWorkers = 0;
        GameDataManager.Instance.pickedRecons = 0;
        GameDataManager.Instance.pickedGatherers = 0;
        SceneManager.LoadScene("DeployMenu");
    }
}
