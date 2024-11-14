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


    
    public void OnDeployButtonClicked() // Activates when the deploy button in DeployMenu.unity ist pressed
    {
        // Checks if the current weight is inside the players maximum weight capacity
        if (GameDataManager.Instance.currentKilogram <= GameDataManager.Instance.maxKilogram)
        {
            deployCutscene.SetActive(true); // Small mp4 currently used as "cutscene" will most likely be removed later
        }
    }



    void OnVideoEnd(VideoPlayer vp) // Activates when the video ends and changes scene
    {
        SceneManager.LoadScene("BaseLevel");
    }
}
