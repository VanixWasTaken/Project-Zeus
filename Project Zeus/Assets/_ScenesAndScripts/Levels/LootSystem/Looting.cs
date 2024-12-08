using NUnit.Framework.Internal;
using UnityEngine;

public class Looting : MonoBehaviour
{

    #region References

    [SerializeField] GameObject lootGO;

    #endregion


    #region Unity Build In

    #region Colliders

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gatherer"))
        {
            GathererLoot lootOnUnit = other.GetComponent<GathererLoot>();
            UnitStateManager unitStateManager = other.GetComponent<UnitStateManager>();

            unitStateManager.collectedLoot++;
            lootOnUnit.lootGO.SetActive(true);

            Destroy(lootGO);

            //Jaspers Code
            CylinderFiller filler = (CylinderFiller)other.transform.GetChild(6).GetComponent("CylinderFiller");
            filler.ChangeFill();
        }
    }

    #endregion

    #endregion
}
