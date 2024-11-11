using UnityEngine;

public class LandingPodButtonScript : MonoBehaviour
{
    [SerializeField] GameObject landingPodMenuGO;
    

    public void OnLandingPodMenuClick()
    {
        if (landingPodMenuGO.activeInHierarchy == false)
        {
            landingPodMenuGO.SetActive(true);
        }
        else
        {
            landingPodMenuGO.SetActive(false);
        }
    }
}
