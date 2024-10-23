using UnityEngine;

public abstract class CommandCenterBaseState
{
    public abstract void EnterState(CommandCenterStateManager player);

    public abstract void UpdateState(CommandCenterStateManager player);
}
