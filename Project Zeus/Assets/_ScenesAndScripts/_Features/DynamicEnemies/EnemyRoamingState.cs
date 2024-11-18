using System.Collections;
using UnityEngine;

public class EnemyRoamingState : EnemyBaseState
{
    private bool detected = false;
    private GameObject detectedObject;

    public override void EnterState(EnemyStateManager _enemy)
    {
        Debug.Log("Roaming!");
        RoamingBehaviour(_enemy);
    }

    public override void UpdateState(EnemyStateManager _enemy)
    {
        if (detectedObject != null && detected)
        {
            _enemy.SetTarget(detectedObject);
        }
    }

    private IEnumerator RoamingBehaviour(EnemyStateManager _enemy)
    {
        // implement behaviour for roaming around their designated area
        yield break;
    }

    public override void OnTriggerEnter(EnemyStateManager _enemy, Collider _collision)
    {
        
    }
}
