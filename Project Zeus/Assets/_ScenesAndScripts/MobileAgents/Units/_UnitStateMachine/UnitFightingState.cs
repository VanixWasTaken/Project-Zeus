using System.Diagnostics;
using static UnitSoundHelper.SoundType;

public class UnitFightingState : UnitBaseState
{

    #region Unity Built-In

    public override void EnterState(UnitStateManager _unit)
    {
        if (_unit.unitClass != UnitStateManager.UnitClass.Fighter)
        {
            _unit.navMeshAgent.ResetPath();
        }

        ResetAnimations(_unit);

        _unit.sound.PlaySoundByType(SHOOTING);
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        LookAtEnemy(_unit); // Look at the nearest enemy

        if (_unit.enemyStateManager.health == 0)
        {
            _unit.sound.StopSoundByType(SHOOTING);
            _unit.SwitchStates(_unit.idleState);
        }
    }

    #endregion


    #region Custom Functions()

    private void LookAtEnemy(UnitStateManager _unit)
    {
        _unit.navMeshAgent.updateRotation = false;

        /// <summary>
        ///             Yoshi: If the class is set to Fighter, just your spine looks into the enmy direction
        ///                    this allows fighter to walk in a different direction, that his upper body looks.
        ///                    All other Units just look at the enemy (cause they cant move while shooting).
        /// </summary>
        if (_unit.unitClass == UnitStateManager.UnitClass.Fighter)
        {
            _unit.spineTransform.LookAt(_unit.nearestEnemyPosition);
        }
        else
        {
            _unit.transform.LookAt(_unit.nearestEnemyPosition);
        }
    }

    private void ResetAnimations(UnitStateManager _unit)
    {
        _unit.animator.SetFloat("anSpeed", 0);
        _unit.animator.SetTrigger("anShouldShoot");
        _unit.animator.SetBool("anIsShooting", true);
    }

    #endregion

}
