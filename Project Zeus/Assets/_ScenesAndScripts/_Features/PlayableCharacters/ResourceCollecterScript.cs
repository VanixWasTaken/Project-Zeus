using UnityEngine;

public class ResourceCollecterScript : MonoBehaviour
{

    UnitWorkerMiningState miningState = new UnitWorkerMiningState();
    UnitIdleState idleState = new UnitIdleState();



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Worker")
        {
            other.transform.LookAt(transform.position); // Turns the player to the ressourceObject


            UnitStateManager unitScript = other.gameObject.GetComponent<UnitStateManager>(); // Get UnitStateManager.cs


            if (unitScript != null) // Switch states inside UnitStateManager
            {
                unitScript.SwitchStates(miningState); // Switch to the miningState in the UnitStateManager
            }
            else
            {
                Debug.LogError("UnitStateManager component not found on the collided object.");
            }

        }
    }
}

   