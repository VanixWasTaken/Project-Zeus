using UnityEngine;

public class ResourceCollecterScript : MonoBehaviour
{
    /*
    public bool isCollecting = false;
    UnitMiningState miningState = new UnitMiningState();
    UnitIdleState idleState = new UnitIdleState();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DronePrefab")
        {
            isCollecting = true;

            // Turns the player to the ressourceObject
            other.transform.LookAt(transform.position);

            // Get UnitStateManager.cs
            UnitStateManager unitScript = other.gameObject.GetComponent<UnitStateManager>();

            // Switch states inside UnitStateManager
            if (unitScript != null)
            {
                unitScript.SwitchStates(miningState); // Switch to the miningState in the UnitStateManager

            } else { Debug.LogWarning("UnitStateManager component not found on the collided object."); }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "DronePrefab")
        {
            isCollecting = false;

            // Get UnitStateManager.cs 
            UnitStateManager unitScript = other.gameObject.GetComponent<UnitStateManager>();

            // Switch states inside UnitSateManager and stop mining animation
            if (unitScript != null)
            {
                unitScript.SwitchStates(idleState); // Switch to the idleState in the UnitStateManager
                unitScript.mAnimator.SetBool("anIsMining", false);

            }
            else { Debug.LogWarning("UnitStateManager component not found on the collided object."); }

        }
    }
    */
}
