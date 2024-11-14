using UnityEngine;

public class WorkerIdleState : WorkerBaseState
{
    public override void EnterState(WorkerStateManager _worker)
    {
        _worker.animator.SetFloat("anSpeed", 0);
    }

    public override void UpdateState(WorkerStateManager _worker)
    {

    }
}
