using UnityEngine;

public class Looting : MonoBehaviour
{

    [SerializeField] GameObject lootGO;


    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gatherer"))
        {
            //other.GetComponent<>
            Destroy(lootGO);
        }
    }

}
