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

            if (unitScript != null && unitScript.CompareTag("Worker")) // Activate one mining device
            {
                //unitScript.
                //unitScript.SwitchStates(miningState);
            }
        }
    }

    #endregion

    #endregion
}

