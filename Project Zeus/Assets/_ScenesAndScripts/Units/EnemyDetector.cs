using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    /*
    [SerializeField] UnitStateManager unitStateManager;

        public void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(unitStateManager.myEnemyTag))
            {
                if (unitStateManager.enemiesInRange.Contains(other.gameObject))
                {
                    return;
                }
                else
                {
                    unitStateManager.enemiesInRange.Add(other.gameObject);
                    if (unitStateManager.currentState != unitStateManager.fightState)
                    {
                        unitStateManager.SwitchStates(unitStateManager.fightState);
                    }
                    else
                    {
                        return;
                    }
                }

            }
        }
        public void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(unitStateManager.myEnemyTag))
            {
                unitStateManager.enemiesInRange.Remove(other.gameObject);
                if (unitStateManager.enemiesInRange.Count <= 0)
                {
                unitStateManager.mAnimator.SetBool("isAttacking", false);
                    unitStateManager.enemiesInRange.Clear();
                    unitStateManager.SwitchStates(unitStateManager.idleState);
                }
            }
        }
    */
}
