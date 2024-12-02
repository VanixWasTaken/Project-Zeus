using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DeployMenuCraftingMenu : MonoBehaviour
{

    #region References

    [SerializeField] TextMeshProUGUI loot;
    [SerializeField] GameObject craftingMenu;

    #endregion



    void Start()
    {
        loot.text = "Loot: " + GameDataManager.Instance.collectedLoot;
    }


    void Update()
    {
        loot.text = "Loot: " + GameDataManager.Instance.collectedLoot;
    }



    #region Custom Functions()

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

    public void OnCraftGathererButtonClicked()
    {
        if (GameDataManager.Instance.collectedLoot > 0)
        {
            GameDataManager.Instance.collectedLoot--;
            GameDataManager.Instance.availableGatherers++;
        }
    }
    #endregion

    #endregion
}
