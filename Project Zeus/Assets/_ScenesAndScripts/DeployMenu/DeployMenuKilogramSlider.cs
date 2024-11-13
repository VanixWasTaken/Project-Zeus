using TMPro;
using UnityEngine;

public class DeployMenuKilogramSlider : MonoBehaviour
{
    float _kilogram;
    float _currentKilogram;
    [SerializeField] TextMeshProUGUI availableKilogramText;
    [SerializeField] GameObject kilogramWarning;

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
