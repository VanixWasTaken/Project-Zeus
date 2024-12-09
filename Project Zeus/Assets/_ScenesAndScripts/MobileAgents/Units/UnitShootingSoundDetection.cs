using UnityEngine;

public class UnitShootingSoundDetection : MonoBehaviour
{

    #region References

    private UnitStateManager unitStateManager;

    #endregion


    #region Unity Build In

    private void Awake()
    {
        if (unitStateManager == null)
        {
            unitStateManager = GetComponentInParent<UnitStateManager>();

            if (unitStateManager == null)
            {
                Debug.Log("UnitStateManager could not be found on UnitVisionDetection");
            }
        }
    }

    #region Colliders

    public void OnTriggerEnter(Collider other)
    {
        unitStateManager.AllertEnemiesInSoundRange(other);
    }

    #endregion

    #endregion


}
