using TMPro;
using UnityEngine;

public class ResourceSystemScript : MonoBehaviour
{

    #region References

    [SerializeField] TextMeshProUGUI mineralsHUDCounter;

    #endregion

    #region Variables

    public int collectedMinerals;
    public ResourceCollecterScript[] resources; // Array to hold multiple resources
    private float timeElapsed = 0f;

    #endregion



    private void Update()
    {
      // CollectResources();
    }


    /*
    private void CollectResources()
    {
        foreach (var resource in resources)
        {
            if (resource.workersInsideRessource > 0)
            {
                timeElapsed += Time.deltaTime;
                collectedMinerals = Mathf.FloorToInt(timeElapsed % 60);

                mineralsHUDCounter.text = collectedMinerals.ToString();
            }
        }
    }
    */
    
}
