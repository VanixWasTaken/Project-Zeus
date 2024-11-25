using UnityEngine;


public class EnemyAttackingState : EnemyBaseState
{
    private UnitStateManager target;

    public override void EnterState(EnemyStateManager _enemy)
    {
        Debug.Log("Attacking!");
        _enemy.transform.LookAt(_enemy.GetTarget().transform);
        _enemy.animator.SetBool("anIsAttacking", true);
        _enemy.animator.SetTrigger("anShouldAttack");
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
        if (_unit != null)
        {
            target = _unit.GetComponent<UnitStateManager>();
        }
        else
        {
            _enemy.SetTarget(null);
            _enemy.SwitchState(_enemy.roamingState);
        }
        

        if (target != null)
        {
            if (target.life <= 0)
            {
                target.Die();
                _enemy.animator.SetBool("anIsAttacking", false);
                _enemy.animator.SetFloat("anSpeed", 0);
                _enemy.SetTarget(null);
                _enemy.UpdateDetectedObjects(_unit);
                _enemy.SwitchState(_enemy.roamingState);
            }
            else if (target.life > 0)
            {
                target.TakeDamage(25f, _enemy);
            } 
        }
        else 
        {
            _enemy.SetTarget(null);
            _enemy.animator.SetBool("anIsAttacking", false);
            _enemy.animator.SetFloat("anSpeed", 0);
            _enemy.UpdateDetectedObjects(_unit);
            _enemy.SwitchState(_enemy.roamingState);
        }
    }
    
}
