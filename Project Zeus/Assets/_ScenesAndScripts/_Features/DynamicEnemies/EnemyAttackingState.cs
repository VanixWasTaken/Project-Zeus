using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class EnemyAttackingState : EnemyBaseState
{
    private UnitStateManager target;

    public override void EnterState(EnemyStateManager _enemy)
    {
        Debug.Log("Attacking!");
        AttackWorker(_enemy, _enemy.GetTarget());
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        
    }
    
    public void AttackWorker(EnemyStateManager _enemy, GameObject _unit)
    {
        target = _unit.GetComponent<UnitStateManager>();

        if (target != null)
        {
            if (target.life <= 0)
            {
                target.Die();
                _enemy.SwitchState(_enemy.roamingState);
            }
            else if (target.life > 0)
            {
                _enemy.StartCoroutine(_enemy.AttackDelay(1.0f));
                target.TakeDamage(25f, _enemy);
            } 
        }

        else 
        {
            _enemy.SwitchState(_enemy.roamingState);
        }
    }
    
}
