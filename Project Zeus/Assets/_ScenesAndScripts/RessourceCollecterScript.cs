using UnityEngine;

public class RessourceCollecterScript : MonoBehaviour
{

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UnitPrefab")
        {
            GameObject unitPrefab = other.gameObject;

            GameObject childObject = unitPrefab.gameObject.GetComponentInChildren<GameObject>();

            if (childObject.gameObject.name == "Drone")
            {
                Debug.Log("Ich bin eine Drone");
            }
        }
    }
}
