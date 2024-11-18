using TMPro;
using UnityEditor;
using UnityEngine;

public class ResourceSystemScript : MonoBehaviour
{

    public int collectedMinerals;
    public ResourceCollecterScript[] resources; // Array to hold multiple resources
    float timeElapsed = 0f;

    [SerializeField] TextMeshProUGUI mineralsHUDCounter;

    private void Update()
    {
       // CollectResources();
    }


    /*
    private void CollectResources()
    {
        foreach (var resource in resources)
        {
            if (resource.isCollecting == true)
            {
                timeElapsed += Time.deltaTime;
                collectedMinerals = Mathf.FloorToInt(timeElapsed % 60);

                mineralsHUDCounter.text = collectedMinerals.ToString();
            }
        }
    }
    */
}
