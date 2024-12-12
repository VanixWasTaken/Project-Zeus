using static FMODUnity.RuntimeManager;
using static FMODAudioData.SoundID;
using System.Diagnostics;

public class UnitDeactivatedState : UnitBaseState
{
    #region Unity Built-In

    public override void EnterState(UnitStateManager _unit)
    {
        _unit.animator.SetFloat("anSpeed", 0); // stop animation
        Deactivate(_unit); // deactivate the unit

        _unit.rightPartUIUnitDescription.isActive = false; // update ui
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        // no need
    }

    #endregion




    #region Custom Functions()

    private void Deactivate(UnitStateManager _unit)
    {
        /// <summary>
        /// Completely stops the unit from moving and plays the transition
        /// to the animation that lets it appear deactivated
        /// </summary>
        
        // Stop Navigation
        _unit.navMeshAgent.isStopped = true;
        _unit.navMeshAgent.ResetPath();
        _unit.StopAllCoroutines();

        // Queue Sound
        if (_unit.audioSheet != null)
        {
            PlayOneShot(_unit.audioSheet.GetSFXByName(SFXUnitPoweringDown));
        }
        else
        {
            UnityEngine.Debug.LogWarning("_Unit.audiosheet is null in UnitDeactivatedState.cs");
        }
        

        // Start "Animation"
        _unit.animator.SetBool("anIsDeactivated", true);
        _unit.animator.SetTrigger("anShouldDeactivate");
    }

    public void Reactivate(UnitStateManager _unit)
    {
        /// <summary>
        /// Restarts the EnergyDepletion Coroutine and transitions
        /// the animation back into the idle
        /// 
        /// Switches States back to Idle
        /// </summary>

        _unit.StartCoroutine(_unit.EnergyDepletion()); // CHANGE SOME STUFF IN THIS LINE COULD HAVE BROKEN SOMETHING

        if (_unit.audioSheet != null)
        {
            PlayOneShot(_unit.audioSheet.GetSFXByName(SFXUnitPoweringUp));
        }
        else
        {
            UnityEngine.Debug.LogWarning("_Unit.audiosheet is null in UnitDeactivatedState.cs");
        }

        _unit.animator.SetBool("anIsDeactivated", false);
        _unit.rightPartUIUnitDescription.isActive = true;
        _unit.SwitchStates(_unit.idleState);
    }

    #endregion
    
}
