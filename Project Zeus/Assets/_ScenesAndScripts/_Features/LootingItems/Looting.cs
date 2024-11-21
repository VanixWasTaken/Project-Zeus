using NUnit.Framework.Internal;
using UnityEngine;

public class Looting : MonoBehaviour
{

    [SerializeField] GameObject lootGO;
    GameObject lootOnUnit;


  

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gatherer"))
        {
            GathererLoot lootOnUnit = other.GetComponent<GathererLoot>();
            
            Debug.Log(lootOnUnit.lootGO.name);

            lootOnUnit.lootGO.SetActive(true);

            Destroy(lootGO);
        }
    }

}
