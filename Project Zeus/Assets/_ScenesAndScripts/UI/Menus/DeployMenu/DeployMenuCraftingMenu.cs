using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeployMenuCraftingMenu : MonoBehaviour
{

    #region References

    [SerializeField] TextMeshProUGUI loot;
    [SerializeField] GameObject craftingMenu;

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
            craftingMenu.SetActive(true);
        }
        else
        {
            craftingMenu.SetActive(false);
        }
    }


    public void OnCraftWorkerButtonClicked()
    {
        if (GameDataManager.Instance.collectedLoot > 0)
        {
            GameDataManager.Instance.collectedLoot--;
            GameDataManager.Instance.availableWorkers++;
        }
    }

    public void OnCraftReconButtonClicked()
    {
        if (GameDataManager.Instance.collectedLoot > 0)
        {
            GameDataManager.Instance.collectedLoot--;
            GameDataManager.Instance.availableRecons++;
        }
    }

    public void OnCraftFighterButtonClicked()
    {
        if (GameDataManager.Instance.collectedLoot > 0)
        {
            GameDataManager.Instance.collectedLoot--;
            GameDataManager.Instance.availableFighters++;
        }
    }
    #endregion

    #endregion

}
