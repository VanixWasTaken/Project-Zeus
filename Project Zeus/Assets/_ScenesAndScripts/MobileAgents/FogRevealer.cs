using FischlWorks_FogWar;
using UnityEngine;

public class FogRevealer : MonoBehaviour
{
    private csFogWar fogWar;
    [SerializeField] UnitStateManager unitStateManager;

    private void Awake()
    {
        if (fogWar == null) // Connects the Fog of War plugin
        {
            fogWar = FindAnyObjectByType<csFogWar>();

            if (fogWar == null)
            {
                Debug.LogError("fogWar on UnitStateManager could not found and assigned");
            }
        }
    }


    
    void Start()
    {
        fogWar.AddFogRevealer(new csFogWar.FogRevealer(this.transform, unitStateManager.visionRange, true)); // Set the Fog of War Revealer
    }

    
    void Update()
    {
        
    }
}
