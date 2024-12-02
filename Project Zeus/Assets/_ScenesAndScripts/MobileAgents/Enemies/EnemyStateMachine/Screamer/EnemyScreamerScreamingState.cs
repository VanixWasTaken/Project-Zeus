using UnityEngine;
using System.Collections.Generic;

public class EnemyScreamerScreamingState : EnemyBaseState
{
    #region Unity Built-In

    public override void EnterState(EnemyStateManager _enemy)
    {
        LightUpAndStartAnims(_enemy); // calls ActivateLights and starts Animations
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        // to make enemy more realistic, turns to scream AT the target
        _enemy.gameObject.transform.LookAt(_enemy.GetTarget().transform.position);
    }

    #region Animations

    // Calls to Alert other enemies when animation is halfway done
    public override void OnScreamHeard(EnemyStateManager _enemy)
    {
        AlertOtherEnemies(_enemy, _enemy.GetTarget());
    }

    // Calls exit after the Scream Animation Finishes
    public override void OnScreamFinished(EnemyStateManager _enemy)
    {
        Exit(_enemy);
    }

    #endregion

    #endregion


    #region Custom Functions

    private void LightUpAndStartAnims(EnemyStateManager _enemy)
    {
        _enemy.ActivateLight();
        _enemy.animator.SetBool("anIsScreaming", true);
        _enemy.animator.SetTrigger("anShouldScream");
    }

    private void AlertOtherEnemies(EnemyStateManager _enemy, GameObject _target)
    {
        /// <summary>
        /// Loops through the EnemiesInRange list and calls the hearscream function
        /// on the object
        /// </summary>
        List<GameObject> enemies = _enemy.GetEnemiesInRange();

        foreach (GameObject obj in enemies)
        {
            EnemyStateManager stateManager = obj.GetComponent<EnemyStateManager>();
            stateManager.HearScream(_target);
        }
    }

    private void Exit(EnemyStateManager _enemy)
    {
        // Stops anim and changes to chasingstate
        _enemy.animator.SetBool("anIsScreaming", false);
        _enemy.SwitchState(_enemy.chasingState);
    }

    #endregion

}
