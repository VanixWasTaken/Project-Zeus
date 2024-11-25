using NUnit.Framework.Internal;
using UnityEngine;

public class Looting : MonoBehaviour
{

    [SerializeField] GameObject lootGO;


  

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

}
