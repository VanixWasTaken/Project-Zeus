using UnityEngine;

public class UnitWorkerMiningState : UnitBaseState
{
    float timeElapsed;

    public override void EnterState(UnitStateManager _unit)
    {
        timeElapsed = 0;
        _unit.animator.SetBool("anIsMining", true);
        _unit.animator.SetTrigger("anShouldMine");
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        timeElapsed += Time.deltaTime;
        _unit.collectedEnergy = Mathf.FloorToInt(timeElapsed % 60) * 2;
    }
}
