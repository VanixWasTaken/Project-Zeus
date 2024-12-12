using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using static FMODUnity.RuntimeManager;
using static FMODAudioData.SoundID;

public class DeployMenuDeployButton : MonoBehaviour
{

    #region References

    [SerializeField] GameObject deployCutscene;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] FMODAudioData UISheet;

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
            PlayOneShot(UISheet.GetSFXByName(SFXMenuUIClick));
            deployCutscene.SetActive(true); // Small mp4 currently used as "cutscene" will most likely be removed later
        }
    }

    #endregion

    #endregion



    #region Custom Functions()

    private void OnVideoEnd(VideoPlayer vp) // Activates when the video ends and changes scene
    {
        UnloadBank("UI");
        SceneManager.LoadScene("BaseLevel");
    }

    #endregion
}
