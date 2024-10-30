using TMPro;
using UnityEditor;
using UnityEngine;

public class ResourceSystemScript : MonoBehaviour
{

    public int collectedMinerals;
    public ResourceCollecterScript resource;
    float timeElapsed = 0f;

    [SerializeField] TextMeshProUGUI mineralsHUDCounter;

    private void Update()
    {
        CollectResources();
    }



    private void CollectResources()
    {
        if (resource.isCollecting == true)
        {
            timeElapsed += Time.deltaTime;
            collectedMinerals = Mathf.FloorToInt(timeElapsed % 60);

            mineralsHUDCounter.text = collectedMinerals.ToString();
            Debug.Log("test14");
        }
    }
}
