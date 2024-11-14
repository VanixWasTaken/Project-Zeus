using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeployMenuKilogramSlider : MonoBehaviour
{
    float _maxKilogram;
    float _currentKilogram;
    [SerializeField] TextMeshProUGUI availableKilogramText;
    [SerializeField] GameObject kilogramWarning;
    [SerializeField] Slider kilogramSlider;

    void Start()
    {
        
    }

    
    void Update()
    {
        _maxKilogram = GameDataManager.Instance.maxKilogram;
        _currentKilogram = GameDataManager.Instance.currentKilogram;

        AdjustKilogramSlider();
        KilogramWarning();
    }


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
}
