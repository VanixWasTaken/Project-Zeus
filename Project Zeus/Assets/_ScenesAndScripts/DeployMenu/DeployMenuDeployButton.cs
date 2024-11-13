using UnityEngine;

public class DeployMenuDeployButton : MonoBehaviour
{
    public void OnDeployButtonClicked()
    {
        if (GameDataManager.Instance.currentKilogram <= GameDataManager.Instance.maxKilogram)
        {
            Debug.Log("Where we droppin boys");
        }
    }
}
