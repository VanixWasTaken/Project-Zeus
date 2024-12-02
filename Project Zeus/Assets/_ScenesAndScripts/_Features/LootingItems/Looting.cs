using NUnit.Framework.Internal;
using UnityEngine;

public class Looting : MonoBehaviour
{

    #region References

    [SerializeField] GameObject lootGO;

    #endregion


    #region Custom Functions()

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gatherer"))
        {
            GathererLoot lootOnUnit = other.GetComponent<GathererLoot>();
            UnitStateManager unitStateManager = other.GetComponent<UnitStateManager>();

            unitStateManager.collectedLoot++;
            lootOnUnit.lootGO.SetActive(true);

            Destroy(lootGO);
        }
    }

    #endregion
}
