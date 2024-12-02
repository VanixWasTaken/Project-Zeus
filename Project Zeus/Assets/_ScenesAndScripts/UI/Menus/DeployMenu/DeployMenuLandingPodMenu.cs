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

    private float _maxKilogram;
    private float _currentKilogram;

    #endregion


    #region Unity Build In

    private void Start()
    {
        loot.text = "Loot\n" + GameDataManager.Instance.collectedLoot; 
    }


    private void Update()
    {
        _maxKilogram = GameDataManager.Instance.maxKilogram;
        _currentKilogram = GameDataManager.Instance.currentKilogram;


        UpdateKilogram(); // Updates the current kilogram in the correct format
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


    #region Custom Functions()

    private void UpdateKilogram()
    {
        landingPodKilogramText.text = "Kilogram\n" + _currentKilogram + " / " + _maxKilogram;
    }

    #endregion
}
