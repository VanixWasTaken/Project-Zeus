using UnityEngine;

public class ResourceCollecterScript : MonoBehaviour
{

    public bool isCollecting = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DronePrefab")
        {
            isCollecting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "DronePrefab")
        {
            isCollecting = false;
        }
    }
}
