using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class DeployMenuDeployButton : MonoBehaviour
{

    #region References

    [SerializeField] GameObject deployCutscene;
    [SerializeField] VideoPlayer videoPlayer;

    #endregion


    #region Unity Build In

    private void Start()
    {
        if (videoPlayer != null) // Subscribe to the loopPointReached event to detect when the video ends
        {
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    #region Buttons

    public void OnDeployButtonClicked() // Activates when the deploy button in DeployMenu.unity ist pressed
    {
        if (GameDataManager.Instance.currentKilogram <= GameDataManager.Instance.maxKilogram) // Checks if the current weight is inside the players maximum weight capacity
        {
            deployCutscene.SetActive(true); // Small mp4 currently used as "cutscene" will most likely be removed later
        }
    }

    #endregion

    #endregion



    #region Custom Functions()

    private void OnVideoEnd(VideoPlayer vp) // Activates when the video ends and changes scene
    {
        SceneManager.LoadScene("BaseLevel");
    }

    #endregion
}
