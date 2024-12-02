using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeployMenuKilogramSlider : MonoBehaviour
{

    #region References

    [SerializeField] TextMeshProUGUI availableKilogramText;
    [SerializeField] GameObject kilogramWarning;
    [SerializeField] Slider kilogramSlider;

    #endregion

    #region Variables

    private float _maxKilogram;
    private float _currentKilogram;

    #endregion


    #region Unity Build In

    private void Update()
    {
        _maxKilogram = GameDataManager.Instance.maxKilogram; // For ease of use a local reference
        _currentKilogram = GameDataManager.Instance.currentKilogram; // For ease of use a local reference


        AdjustKilogramSlider(); // Adjust the Slider to represant the current percent of usage of the available weight

        KilogramWarning(); // Pops a little warning that shows the player he is currently overweight
    }

    #endregion


    #region Custom Functions()

    private void AdjustKilogramSlider()
    {
        float percent = (_currentKilogram / _maxKilogram) * 100;
        kilogramSlider.value = percent / 100f;
        availableKilogramText.text = _currentKilogram + " / " + _maxKilogram;
    }

    private void KilogramWarning()
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
