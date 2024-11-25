using TMPro;
using UnityEngine;

public class DeployMenuLandingPodMenu : MonoBehaviour
{
    [SerializeField] GameObject landingPodMenuGO;
    float _maxKilogram;
    float _currentKilogram;
    [SerializeField] TextMeshProUGUI landingPodKilogramText;
    [SerializeField] TextMeshProUGUI loot;


    void Start()
    {
        loot.text = "Loot\n" + GameDataManager.Instance.collectedLoot; 
    }


    void Update()
    {
        _maxKilogram = GameDataManager.Instance.maxKilogram;
        _currentKilogram = GameDataManager.Instance.currentKilogram;

        UpdateKilogram(); // Updates the current kilogram in the correct format
    }




    void UpdateKilogram()
    {
        landingPodKilogramText.text = "Kilogram\n" + _currentKilogram + " / " + _maxKilogram;
    }

    public void OnLandingPodMenuClick()
    {
        if (landingPodMenuGO.activeInHierarchy == false)
        {
            landingPodMenuGO.SetActive(true);
        }
        else
        {
            landingPodMenuGO.SetActive(false);
        }
    } // Shows the pod menu when the LandingPodButton is pressed inside DeployMenu

    
}
