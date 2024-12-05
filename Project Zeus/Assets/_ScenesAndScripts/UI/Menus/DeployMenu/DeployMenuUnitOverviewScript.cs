using TMPro;
using UnityEngine;
using static FMODUnity.RuntimeManager;
using static FMODAudioData.SoundID;

public class DeployMenuUnitOverviewScript : MonoBehaviour
{

    #region References

    [SerializeField] TextMeshProUGUI workersText;
    [SerializeField] TextMeshProUGUI reconsText;
    [SerializeField] TextMeshProUGUI fightersText;
    [SerializeField] FMODAudioData audioData;

    #endregion

    #region Variables

    private int currentPickedWorkers = 0;
    private int availableWorkers;
    private int currentPickedRecons = 0;
    private int availableRecons;
    private int currentPickedFighters = 0;
    private int availableFighters;

    #endregion


    #region Unity Build In

    private void Awake()
    {
        // load FMOD bank to be able to play UI sounds
        LoadBank("UI");
    }

    private void Start()
    {
        // For ease of use a local reference
        availableWorkers = GameDataManager.Instance.availableWorkers;
        availableRecons = GameDataManager.Instance.availableRecons;
        availableFighters = GameDataManager.Instance.availableFighters;


        UpdateTexts(); // Reads the current units available and correctly formats the texts
    }



    private void Update()
    {
        UpdateTexts(); // Reads the current units available and correctly formats the texts  
    }

    #region Buttons

    /// <summary>
    /// All units each have two buttons, one for adding and one for subtracting the count inside the text.
    /// Can't count above maximum individual unit count or less than zero.
    /// </summary>
    public void OnWorkersAddClicked()
    {
        if (currentPickedWorkers < availableWorkers)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIClick));
            currentPickedWorkers++;
            GameDataManager.Instance.IncreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedWorkers++;
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIError));
        }
        workersText.text = ("Workers\t\t: " + currentPickedWorkers + " / " + availableWorkers);
    }
    public void OnWorkersSubtractClicked()
    {
        if (currentPickedWorkers > 0)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIDeselect));
            currentPickedWorkers--;
            GameDataManager.Instance.DecreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedWorkers--;
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIError));
        }
        workersText.text = ("Workers\t\t: " + currentPickedWorkers + " / " + availableWorkers);
    }

    public void OnReconsAddClicked()
    {
        if (currentPickedRecons < availableRecons)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIClick));
            currentPickedRecons++;
            GameDataManager.Instance.IncreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedRecons++;
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIError));
        }
        reconsText.text = ("Recons\t\t: " + currentPickedRecons + " / " + availableRecons);
    }
    public void OnReconsSubtractClicked()
    {
        if (currentPickedRecons > 0)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIDeselect));
            currentPickedRecons--;
            GameDataManager.Instance.DecreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedRecons--;
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIError));
        }
        reconsText.text = ("Recons\t\t: " + currentPickedRecons + " / " + availableRecons);
    }

    public void OnFightersAddClicked()
    {
        
        if (currentPickedFighters < availableFighters)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIClick));
            currentPickedFighters++;
            GameDataManager.Instance.IncreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedFighters++;
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIError));
        }
        fightersText.text = ("Fighters\t\t: " + currentPickedFighters + " / " + availableFighters);
    }
    public void OnFightersSubtractClicked()
    {
        if (currentPickedFighters > 0)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIDeselect));
            currentPickedFighters--;
            GameDataManager.Instance.DecreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedFighters--;
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIError));
        }
        fightersText.text = ("Fighters\t\t: " + currentPickedFighters + " / " + availableFighters);
    }

    #endregion

    #endregion


    #region Custom Functions()

    private void UpdateTexts()
    {
        workersText.text = ("Workers\t\t: " + currentPickedWorkers + " / " + GameDataManager.Instance.availableWorkers);
        reconsText.text = ("Recons\t\t: " + currentPickedRecons + " / " + GameDataManager.Instance.availableRecons);
        fightersText.text = ("Fighters\t\t: " + currentPickedFighters + " / " + GameDataManager.Instance.availableFighters);
    }

    #endregion
}
