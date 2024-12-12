using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static FMODUnity.RuntimeManager;
using static FMODAudioData.SoundID;
using NUnit.Framework.Constraints;

public class DeployMenuKilogramSlider : MonoBehaviour
{

    #region References

    [SerializeField] TextMeshProUGUI availableKilogramText;
    [SerializeField] GameObject kilogramWarning;
    [SerializeField] Slider kilogramSlider;
    [SerializeField] FMODAudioData audioSheet;

    #endregion

    #region Variables

    private float _maxKilogram;
    private float _currentKilogram;
    private bool playOnce = false;

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

            if (!playOnce)
            {
                PlayOneShot(audioSheet.GetSFXByName(SFXDeployMenuUITooHeavy));
                playOnce = true;
            }
        }
        else
        {
            kilogramWarning.SetActive(false);
            playOnce = false;
        }
    }

    #endregion
}
