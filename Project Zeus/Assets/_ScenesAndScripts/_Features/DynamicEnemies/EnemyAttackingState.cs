using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    private WorkerStateManager target;

    public override void EnterState(EnemyStateManager _enemy)
    {
        Debug.Log("Attacking!");
        AttackWorker(_enemy, target);
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        throw new System.NotImplementedException();
    }

    private void AttackWorker(EnemyStateManager _enemy, WorkerStateManager _worker = null)
    {

    }
}
