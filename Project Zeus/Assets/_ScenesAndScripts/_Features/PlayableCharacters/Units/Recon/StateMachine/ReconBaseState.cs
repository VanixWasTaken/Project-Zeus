using UnityEngine;

public abstract class ReconBaseState
{
    public abstract void EnterState(ReconStateManager _recon);

    public abstract void UpdateState(ReconStateManager _recon);

}
