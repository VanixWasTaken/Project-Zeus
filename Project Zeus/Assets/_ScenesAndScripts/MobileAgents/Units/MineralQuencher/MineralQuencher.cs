using UnityEngine;

public class MineralQuencher : MonoBehaviour
{

    #region References

    private GameObject workerGO;
    private UnitStateManager unitStateManager;

    #endregion

    #region Variables

    private bool assignedWorker = false;
    private bool isCollecting = false;
    public int collectedMinerals;
    private float mineralAccumulator = 0f; // Used to tranfser float into int over time

    #endregion



    #region Unity Build In

    private void Update()
    {
        if (assignedWorker)
        {
            transform.position = workerGO.transform.position;
        }   

        if (isCollecting)
        {
            CollectMineral();
        }
    }


    #region Colliders

    public void WorkerEntered(Collider other)
    {
        unitStateManager = other.GetComponent<UnitStateManager>();

        if (!unitStateManager.holdsMineralQuencher)
        {
            assignedWorker = true;
            workerGO = other.gameObject;

            unitStateManager.holdsMineralQuencher = true;
        }
    }

    public void MineralEntered(Collider other)
    {
        float randX = Random.Range(-1f, 1f);
        float randZ = Random.Range(-1f, 1f);

        Vector3 newPos = other.transform.position + new Vector3(randX, 2, randZ);
        transform.position = newPos;

        assignedWorker = false;
        isCollecting = true;

        unitStateManager.holdsMineralQuencher = false;
    }

    #endregion

    #endregion



    #region Custom Functions()

    private void CollectMineral()
    {
        mineralAccumulator += Time.deltaTime;

        if (mineralAccumulator >= 1f)
        {
            collectedMinerals += 1;
            mineralAccumulator = 0f;
        }
    }

    #endregion
}
