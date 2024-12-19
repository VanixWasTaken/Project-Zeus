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
        if (other.CompareTag("Worker"))
        {
            UnitLoot lootOnUnit = other.GetComponent<UnitLoot>();
            UnitStateManager unitStateManager = other.GetComponent<UnitStateManager>();

            unitStateManager.collectedLoot++;
            lootOnUnit.lootGO.SetActive(true);

            Destroy(lootGO);
        }
    }

    #endregion

    #endregion
}
