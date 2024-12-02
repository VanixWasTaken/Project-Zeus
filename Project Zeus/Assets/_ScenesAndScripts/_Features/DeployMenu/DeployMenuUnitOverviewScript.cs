using TMPro;
using UnityEngine;

public class DeployMenuUnitOverviewScript : MonoBehaviour
{

    #region References

    [SerializeField] TextMeshProUGUI workersText;
    [SerializeField] TextMeshProUGUI reconsText;
    [SerializeField] TextMeshProUGUI gatherersText;

    #endregion

    #region Variables

    int currentPickedWorkers = 0;
    int availableWorkers;
    int currentPickedRecons = 0;
    int availableRecons;
    int currentPickedGatherers = 0;
    int availableGatherers;

    #endregion


    void Start()
    {
        // For ease of use a local reference
        availableWorkers = GameDataManager.Instance.availableWorkers;
        availableRecons = GameDataManager.Instance.availableRecons;
        availableGatherers = GameDataManager.Instance.availableGatherers;


        UpdateTexts(); // Reads the current units available and correctly formats the texts
    }



    void Update()
    {
        UpdateTexts(); // Reads the current units available and correctly formats the texts
       
    }



    #region Custom Functions()

    void UpdateTexts()
    {
        workersText.text = ("Workers\t\t: " + currentPickedWorkers + " / " + GameDataManager.Instance.availableWorkers);
        reconsText.text = ("Recons\t\t: " + currentPickedRecons + " / " + GameDataManager.Instance.availableRecons);
        gatherersText.text = ("Gatherers\t\t: " + currentPickedGatherers + " / " + GameDataManager.Instance.availableGatherers);
    }

    #region Buttons

    /// <summary>
    /// All units each habe two buttons, one for adding and one for subtracting the count inside the text.
    /// Can't count above maximum individual unit count or less than zero.
    /// </summary>
    public void OnWorkersAddClicked()
    {
        if (currentPickedWorkers < availableWorkers)
        {
            currentPickedWorkers++;
            GameDataManager.Instance.IncreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedWorkers++;
        }
        workersText.text = ("Workers\t\t: " + currentPickedWorkers + " / " + availableWorkers);
    }
    public void OnWorkersSubtractClicked()
    {
        if (currentPickedWorkers > 0)
        {
            currentPickedWorkers--;
            GameDataManager.Instance.DecreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedWorkers--;
        }
        workersText.text = ("Workers\t\t: " + currentPickedWorkers + " / " + availableWorkers);
    }

    public void OnReconsAddClicked()
    {
        if (currentPickedRecons < availableRecons)
        {
            currentPickedRecons++;
            GameDataManager.Instance.IncreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedRecons++;
        }
        reconsText.text = ("Recons\t\t: " + currentPickedRecons + " / " + availableRecons);
    }
    public void OnReconsSubtractClicked()
    {
        if (currentPickedRecons > 0)
        {
            currentPickedRecons--;
            GameDataManager.Instance.DecreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedRecons--;
        }
        reconsText.text = ("Recons\t\t: " + currentPickedRecons + " / " + availableRecons);
    }

    public void OnGatherersAddClicked()
    {
        if (currentPickedGatherers < availableGatherers)
        {
            currentPickedGatherers++;
            GameDataManager.Instance.IncreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedGatherers++;
        }
        gatherersText.text = ("Gatherers\t\t: " + currentPickedGatherers + " / " + availableGatherers);
    }
    public void OnGatherersSubtractClicked()
    {
        if (currentPickedGatherers > 0)
        {
            currentPickedGatherers--;
            GameDataManager.Instance.DecreaseCurrentKilogram(10);
            GameDataManager.Instance.pickedGatherers--;
        }
        gatherersText.text = ("Gatherers\t\t: " + currentPickedGatherers + " / " + availableGatherers);
    }

    #endregion

    #endregion
}
