using UnityEngine;

public abstract class UnitBaseState
{
    public abstract void EnterState(UnitStateManager _unit);

    public abstract void UpdateState(UnitStateManager _unit);

}
