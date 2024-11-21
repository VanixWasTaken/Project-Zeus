using UnityEngine;

public class UnitDeactivatedState : UnitBaseState
{
    public override void EnterState(UnitStateManager _unit)
    {
        _unit.animator.SetFloat("anSpeed", 0);
        Deactivate(_unit);

        _unit.rightPartUIUnitDescription.isActive = false;
    }

    public override void UpdateState(UnitStateManager _unit)
    {
        
    }

    private void Deactivate(UnitStateManager _unit)
    {
        _unit.navMeshAgent.isStopped = true;
        _unit.navMeshAgent.ResetPath();
        _unit.StopAllCoroutines();
        _unit.animator.SetBool("anIsDeactivated", true);
        _unit.animator.SetTrigger("anShouldDeactivate");
    }

    public void Reactivate(UnitStateManager _unit)
    {
        _unit.StartCoroutine(_unit.EnergyDepletion(_unit.energyDepletionInterval));
        _unit.animator.SetBool("anIsDeactivated", false);
        _unit.rightPartUIUnitDescription.isActive = true;
        _unit.SwitchStates(_unit.idleState);
    }
}
