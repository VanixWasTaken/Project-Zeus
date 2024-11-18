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

    public Transform AquireWorkerPos(EnemyStateManager _enemy)
    {
        GameObject workerObject = _enemy.GetTarget();
        Transform workerPosition = workerObject.GetComponent<Transform>();
        Debug.Log(workerPosition.ToString());
        return workerPosition;
    }

    private void ChasePlayer(EnemyStateManager _enemy)
    {
        AquireWorkerPos(_enemy);
    }
}
