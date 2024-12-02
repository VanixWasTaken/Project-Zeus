using TMPro;
using UnityEngine;

public class DeployMenuLandingPodMenu : MonoBehaviour
{

    #region References

    [SerializeField] GameObject landingPodMenuGO;
    [SerializeField] TextMeshProUGUI landingPodKilogramText;
    [SerializeField] TextMeshProUGUI loot;

    #endregion

    #region Variables

    float _maxKilogram;
    float _currentKilogram;

    #endregion

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



    #region Custom Functions()

    void UpdateKilogram()
    {
        landingPodKilogramText.text = "Kilogram\n" + _currentKilogram + " / " + _maxKilogram;
    }

    #region Buttons

    public void OnLandingPodMenuClick() // Displays the pod menu when the LandingPodButton is pressed inside DeployMenu
    {
        if (landingPodMenuGO.activeInHierarchy == false)
        {
            landingPodMenuGO.SetActive(true);
        }
        else
        {
            landingPodMenuGO.SetActive(false);
        }
    } 

    #endregion

    #endregion
}
