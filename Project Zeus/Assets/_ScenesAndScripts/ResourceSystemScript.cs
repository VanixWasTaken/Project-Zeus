using TMPro;
using UnityEditor;
using UnityEngine;

public class ResourceSystemScript : MonoBehaviour
{

    public int collectedResources;
    public ResourceCollecterScript resource;
    float timeElapsed = 0f;


    private void Update()
    {
        CollectResources();
    }



    private void CollectResources()
    {
        if (resource.isCollecting == true)
        {
            timeElapsed += Time.deltaTime;
            collectedResources = Mathf.FloorToInt(timeElapsed % 60);
        }
    }
}
