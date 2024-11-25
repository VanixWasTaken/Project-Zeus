using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;


public class EnemyAttackingState : EnemyBaseState
{
    private UnitStateManager target;

    public override void EnterState(EnemyStateManager _enemy)
    {
        Debug.Log("Attacking!");
        _enemy.animator.SetBool("anIsAttacking", true);
        _enemy.animator.SetTrigger("anShouldAttack");
        AttackWorker(_enemy, _enemy.GetTarget());
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        
    }

    public override void OnUnitHit(EnemyStateManager _enemy)
    {
        AttackWorker(_enemy, _enemy.GetTarget());
    }

    public void AttackWorker(EnemyStateManager _enemy, GameObject _unit)
    {
        target = _unit.GetComponent<UnitStateManager>();

        _enemy.transform.LookAt(target.transform.position);

        if (target != null)
        {
            if (target.life <= 0)
            {
                target.Die();
                _enemy.animator.SetBool("anIsAttacking", false);
                _enemy.animator.SetFloat("anSpeed", 0);
                _enemy.SwitchState(_enemy.roamingState);
            }
            else if (target.life > 0)
            {
                target.TakeDamage(25f, _enemy);
            } 
        }

        else 
        {
            _enemy.SwitchState(_enemy.roamingState);
        }
    }
    
}
