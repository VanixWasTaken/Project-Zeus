using System.Collections;
using System.Collections.Generic;
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

        if (target.life > 0)
        {
            target.TakeDamage(); // Fill TakeDamage with 25 later 
            Debug.Log("The target has " + target.life + " lifepoints.");
        }

        else if (target.life <= 0) 
        {
            _enemy.SwitchState(_enemy.roamingState);
        }

        _enemy.StartCoroutine(_enemy.AttackDelay(1.0f));

    }
    
}
