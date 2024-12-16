using UnityEngine;

public class MineralQuencherHitboxDetector : MonoBehaviour
{

    #region Reference

    [SerializeField] MineralQuencher mineralQuencher;

    #endregion



    #region Unity Build In

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Worker"))
        {
            mineralQuencher.WorkerEntered(other);
        }

        
        if (other.CompareTag("Mineral"))
        {
            mineralQuencher.MineralEntered(other);
        }
    }

    #endregion
}
