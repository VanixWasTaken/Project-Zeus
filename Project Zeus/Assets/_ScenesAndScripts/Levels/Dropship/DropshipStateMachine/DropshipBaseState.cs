using UnityEngine;

public abstract class DropshipBaseState
{
    public abstract void EnterState(DropshipStateManager player);

    public abstract void UpdateState(DropshipStateManager player);
}
