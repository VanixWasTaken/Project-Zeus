using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class EnemyScreamerScreamingState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager _enemy)
    {
        _enemy.animator.SetBool("anIsScreaming", true);
        _enemy.animator.SetTrigger("anShouldScream");
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {

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
        // replace this later with enemies that are inside the radius, did not have time to implement properly
        // since the enemiesInRange List did not populate for some reason
        /*
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject obj in enemies)
        {
            EnemyStateManager stateManager = obj.GetComponent<EnemyStateManager>();
            stateManager.HearScream(_target);
        }
        */
    }

    private void Exit(EnemyStateManager _enemy)
    {
        _enemy.SwitchState(_enemy.chasingState);
    }
}
