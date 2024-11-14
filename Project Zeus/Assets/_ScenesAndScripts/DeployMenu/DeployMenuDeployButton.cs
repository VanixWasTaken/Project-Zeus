using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DeployMenuDeployButton : MonoBehaviour
{

    [SerializeField] GameObject deployCutscene;
    [SerializeField] VideoPlayer videoPlayer;


    void Start()
    {
        if (videoPlayer != null)
        {
            // Subscribe to the loopPointReached event to detect when the video ends
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }


    public void OnDeployButtonClicked()
    {
        if (GameDataManager.Instance.currentKilogram <= GameDataManager.Instance.maxKilogram)
        {
            deployCutscene.SetActive(true);
        }
    }



    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("Video has ended!");
        SceneManager.LoadScene("BaseLevel");
    }
}
