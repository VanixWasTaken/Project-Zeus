using TMPro;
using UnityEngine;

public class DeployMenuUnitOverviewScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI workersText;
    [SerializeField] TextMeshProUGUI reconsText;
    [SerializeField] TextMeshProUGUI gatherersText;

    int currentPickedWorkers = 0;
    int availableWorkers;
    int currentPickedRecons = 0;
    int availableRecons;
    int currentPickedGatherers = 0;
    int availableGatherers;

    void Start()
    {
        availableWorkers = GameDataManager.Instance.availableWorkers;
        availableRecons = GameDataManager.Instance.availableRecons;
        availableGatherers = GameDataManager.Instance.availableGatherers;

        workersText.text = ("Workers    : " + currentPickedWorkers + " / " + availableWorkers);
        reconsText.text = ("Recons  : " + currentPickedRecons + " / " + availableRecons);
        gatherersText.text = ("Gatherers    : " + currentPickedGatherers + " / " + availableGatherers);
    }

    void Update()
    {
        
    }

    public void OnWorkersAddClicked()
    {
        if (currentPickedWorkers < availableWorkers)
        {
            currentPickedWorkers++;
        }
        workersText.text = ("Workers    : " + currentPickedWorkers + " / " + availableWorkers);
    }
    public void OnWorkersSubtractClicked()
    {
        if (currentPickedWorkers > 0)
        {
            currentPickedWorkers--;
        }
        workersText.text = ("Workers    : " + currentPickedWorkers + " / " + availableWorkers);
    }

    public void OnReconsAddClicked()
    {
        if (currentPickedRecons < availableRecons)
        {
            currentPickedRecons++;
        }
        reconsText.text = ("Recons  : " + currentPickedRecons + " / " + availableRecons);
    }
    public void OnReconsSubtractClicked()
    {
        if (currentPickedRecons > 0)
        {
            currentPickedRecons--;
        }
        reconsText.text = ("Recons : " + currentPickedRecons + " / " + availableRecons);
    }

    public void OnGatherersAddClicked()
    {
        if (currentPickedGatherers < availableGatherers)
        {
            currentPickedGatherers++;
        }
        gatherersText.text = ("Gatherers    : " + currentPickedGatherers + " / " + availableGatherers);
    }
    public void OnGatherersSubtractClicked()
    {
        if (currentPickedGatherers > 0)
        {
            currentPickedGatherers--;
        }
        gatherersText.text = ("Gatherers    : " + currentPickedGatherers + " / " + availableGatherers);
    }
}