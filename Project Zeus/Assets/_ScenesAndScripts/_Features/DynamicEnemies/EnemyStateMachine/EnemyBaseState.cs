using UnityEngine;

public abstract class EnemyBaseState
{
    public abstract void EnterState(EnemyStateManager _enemy);

    public abstract void UpdateState(EnemyStateManager _enemy);

    public virtual void OnTriggerEnter(EnemyStateManager _enemy, Collider _collision) { }

    public virtual void OnTriggerExit(EnemyStateManager _enemy, Collider _collision) { }

    public virtual void OnUnitHit(EnemyStateManager _enemy) { }

    public virtual void OnScreamHeard(EnemyStateManager _enemy) { }

    public virtual void OnScreamFinished(EnemyStateManager _enemy) { }
}
