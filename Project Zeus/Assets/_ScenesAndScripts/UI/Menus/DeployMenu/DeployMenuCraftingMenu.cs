using TMPro;
using UnityEngine;
using static FMODUnity.RuntimeManager;
using static FMODAudioData.SoundID;

public class DeployMenuCraftingMenu : MonoBehaviour
{

    #region References

    [SerializeField] TextMeshProUGUI loot;
    [SerializeField] GameObject craftingMenu;
    [SerializeField] FMODAudioData audioData;

    #endregion


    #region Unity Build In
    private void Start()
    {
        loot.text = "Loot: " + GameDataManager.Instance.collectedLoot;
    }


    private void Update()
    {
        loot.text = "Loot: " + GameDataManager.Instance.collectedLoot;
    }

    #region Buttons
    public void OnCraftingMenuButtonClicked()
    {
        if (craftingMenu.activeSelf == false)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIClick));
            craftingMenu.SetActive(true);
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIDeselect));
            craftingMenu.SetActive(false);
        }
    }


    public void OnCraftWorkerButtonClicked()
    {
        if (GameDataManager.Instance.collectedLoot > 0)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIClick));
            GameDataManager.Instance.collectedLoot--;
            GameDataManager.Instance.availableWorkers++;
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIError));
        }
    }

    public void OnCraftReconButtonClicked()
    {
        if (GameDataManager.Instance.collectedLoot > 0)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIClick));
            GameDataManager.Instance.collectedLoot--;
            GameDataManager.Instance.availableRecons++;
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIError));
        }
    }

    public void OnCraftFighterButtonClicked()
    {
        if (GameDataManager.Instance.collectedLoot > 0)
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIClick));
            GameDataManager.Instance.collectedLoot--;
            GameDataManager.Instance.availableFighters++;
        }
        else
        {
            PlayOneShot(audioData.GetSFXByName(SFXMenuUIError));
        }
    }
    #endregion

    #endregion

}
