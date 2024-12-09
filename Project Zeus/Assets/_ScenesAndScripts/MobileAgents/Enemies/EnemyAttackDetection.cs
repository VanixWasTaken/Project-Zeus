using UnityEngine;

public class EnemyAttackDetection : MonoBehaviour
{

    #region References

    private EnemyStateManager enemyStateManager;

    #endregion




    #region Unity Build In

    private void Awake()
    {
        if (enemyStateManager == null)
        {
            enemyStateManager = GetComponentInParent<EnemyStateManager>();

            if (enemyStateManager == null)
            {
                Debug.Log("EnemyStateManager could not be found on EnemyVisionDetection");
            }
        }
    }


    #region Colliders

    private void OnTriggerEnter(Collider other)
    {
        enemyStateManager.ShouldAttackUnits(other, true);
    }

    private void OnTriggerExit(Collider other)
    {
        enemyStateManager.ShouldAttackUnits(other, false);
    }

    #endregion

    #endregion




}
