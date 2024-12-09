using UnityEngine;

public class EnemyVisionDetection : MonoBehaviour
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
        enemyStateManager.DetectUnits(other);
    }

    private void OnTriggerExit(Collider other)
    {
        enemyStateManager.LooseUnits(other);
    }

    #endregion

    #endregion



}
