using UnityEngine;

public abstract class WorkerBaseState
{
    public abstract void EnterState(WorkerStateManager _worker);

    public abstract void UpdateState(WorkerStateManager _worker);

}
