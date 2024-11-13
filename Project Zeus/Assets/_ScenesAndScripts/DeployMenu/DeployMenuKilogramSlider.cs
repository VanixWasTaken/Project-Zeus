using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeployMenuKilogramSlider : MonoBehaviour
{
    float _kilogram;
    float _currentKilogram;
    [SerializeField] TextMeshProUGUI availableKilogramText;
    [SerializeField] GameObject kilogramWarning;
    [SerializeField] Slider kilogramSlider;

    void Start()
    {
        
    }

    
    void Update()
    {
        _kilogram = GameDataManager.Instance.maxKilogram;
        _currentKilogram = GameDataManager.Instance.currentKilogram;

        AdjustKilogramSlider();
        KilogramWarning();
    }


    public void AdjustKilogramSlider()
    {
        float percent = (_currentKilogram / _kilogram) * 100;
        kilogramSlider.value = percent / 100f;
        availableKilogramText.text = _currentKilogram + " / " + _kilogram;
    }

    void KilogramWarning()
    {
        if (_currentKilogram > _kilogram)
        {
            kilogramWarning.SetActive(true);
        }
        else
        {
            kilogramWarning.SetActive(false);
        }
    }
}
