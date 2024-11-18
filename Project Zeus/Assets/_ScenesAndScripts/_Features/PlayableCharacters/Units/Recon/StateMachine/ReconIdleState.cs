using UnityEngine;

public class ReconIdleState : ReconBaseState
{
    public override void EnterState(ReconStateManager _worker)
    {
        _worker.animator.SetFloat("anSpeed", 0);
    }

    public override void UpdateState(ReconStateManager _worker)
    {

    }
}
