using UnityEngine;

public class EnemyChasingState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager _enemy)
    {
        Debug.Log("Chasing!");
        ChasePlayer(_enemy);
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        // check for exit conditions, e.g. worker in range for attack or out of range for roaming
    }

    private void ChasePlayer(EnemyStateManager _enemy, WorkerStateManager _worker = null)
    {
        // get position of worker object and set destination for chasing the worker around
    }
}
