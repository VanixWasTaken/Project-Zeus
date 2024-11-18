using System.Collections;
using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager _enemy)
    {
        Debug.Log("Roaming!");
        RoamingBehaviour(_enemy);
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        // check for exit conditions --> player in sight / attacking range
    }

    private IEnumerator RoamingBehaviour(EnemyStateManager _enemy)
    {
        // implement behaviour for roaming around their designated area
        yield break;
    }
}
