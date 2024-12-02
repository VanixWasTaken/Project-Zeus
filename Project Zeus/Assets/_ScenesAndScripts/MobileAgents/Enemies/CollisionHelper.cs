using UnityEngine;

public class CollisionHelper : MonoBehaviour
{
    #region Unity Built-In

    private void OnTriggerEnter(Collider other)
    {
        UnitStateManager unit = GetComponentInParent<UnitStateManager>();
        unit.GathererShouldFight(other);
    }

    private void OnTriggerExit(Collider other)
    {
        UnitStateManager unit = GetComponentInParent<UnitStateManager>();
        unit.GathererRangeExited(other);
    }

    #endregion
}
