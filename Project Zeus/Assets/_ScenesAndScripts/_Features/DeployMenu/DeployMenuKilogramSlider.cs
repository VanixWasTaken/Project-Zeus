using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeployMenuKilogramSlider : MonoBehaviour
{

    #region References

    float _maxKilogram;
    float _currentKilogram;
    [SerializeField] TextMeshProUGUI availableKilogramText;
    [SerializeField] GameObject kilogramWarning;
    [SerializeField] Slider kilogramSlider;

    #endregion


    void Update()
    {
        _maxKilogram = GameDataManager.Instance.maxKilogram; // For ease of use a local reference
        _currentKilogram = GameDataManager.Instance.currentKilogram; // For ease of use a local reference


        AdjustKilogramSlider(); // Adjust the Slider to represant the current percent of usage of the available weight

        KilogramWarning(); // Pops a little warning that shows the player he is currently overweight
    }




    #region Custom Functions()

    public void AdjustKilogramSlider()
    {
        float percent = (_currentKilogram / _maxKilogram) * 100;
        kilogramSlider.value = percent / 100f;
        availableKilogramText.text = _currentKilogram + " / " + _maxKilogram;
    }

    void KilogramWarning()
    {
        if (_currentKilogram > _maxKilogram)
        {
            kilogramWarning.SetActive(true);
        }
        else
        {
            kilogramWarning.SetActive(false);
        }
    }

    #endregion
}
