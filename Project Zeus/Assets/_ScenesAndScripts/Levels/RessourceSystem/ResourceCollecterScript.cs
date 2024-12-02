using UnityEngine;

public class ResourceCollecterScript : MonoBehaviour
{


    #region Unity Build In

    #region Collider
    
    private void OnTriggerEnter(Collider other)
    {
        /// <summary>
        /// Gets the entered Worker and switches his state to the MiningState
        /// </summary>

        if (other.tag == "Worker")
        {
            other.transform.LookAt(transform.position); // Turns the player to the ressourceObject


            UnitStateManager unitScript = other.gameObject.GetComponent<UnitStateManager>(); // Get UnitStateManager.cs
            UnitWorkerMiningState miningState = new UnitWorkerMiningState();

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

    #endregion

    #endregion
}

