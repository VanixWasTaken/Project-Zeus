using TMPro;
using UnityEngine;

public class DeployMenuLandingPodMenu : MonoBehaviour
{
    [SerializeField] GameObject landingPodMenuGO;
    float _kilogram;
    float _currentKilogram;
    [SerializeField] TextMeshProUGUI landingPodKilogramText;

    void Update()
    {
        _kilogram = GameDataManager.Instance.maxKilogram;
        _currentKilogram = GameDataManager.Instance.currentKilogram;

        UpdateKilogram();
    }


    void UpdateKilogram()
    {
        landingPodKilogramText.text = "Kilogram\n" + _currentKilogram + " / " + _kilogram;
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
    }

    
}
