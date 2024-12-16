using UnityEngine;

public class UnitWorkerMiningState : UnitBaseState
{

    #region Variables

    float timeElapsed;

    #endregion



    #region Unity Built-In

    public override void EnterState(UnitStateManager _unit)
    {
        timeElapsed = 0; // reset the timeElapsed

        // Starts the animations
        _unit.animator.SetBool("anIsMining", true);
        _unit.animator.SetTrigger("anShouldMine");


    }

    public override void UpdateState(UnitStateManager _unit)
    {
        // Lets the unit collect energy coupled to time
        timeElapsed += Time.deltaTime;
        _unit.collectedEnergy = Mathf.FloorToInt(timeElapsed % 60) * 2;
    }

    #endregion
}
