using UnityEngine;
using System.Collections.Generic;

public class EnemyScreamerScreamingState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager _enemy)
    {
        _enemy.ActivateLight();
        _enemy.animator.SetBool("anIsScreaming", true);
        _enemy.animator.SetTrigger("anShouldScream");
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        _enemy.gameObject.transform.LookAt(_enemy.GetTarget().transform.position);
    }

    public override void OnScreamHeard(EnemyStateManager _enemy)
    {
        AlertOtherEnemies(_enemy, _enemy.GetTarget());
    }

    public override void OnScreamFinished(EnemyStateManager _enemy)
    {
        Exit(_enemy);
    }

    private void AlertOtherEnemies(EnemyStateManager _enemy, GameObject _target)
    {
        List<GameObject> enemies = _enemy.GetEnemiesInRange();

        foreach (GameObject obj in enemies)
        {
            EnemyStateManager stateManager = obj.GetComponent<EnemyStateManager>();
            stateManager.HearScream(_target);
        }
    }

    private void Exit(EnemyStateManager _enemy)
    {
        _enemy.animator.SetBool("anIsScreaming", false);
        _enemy.SwitchState(_enemy.chasingState);
    }
}
