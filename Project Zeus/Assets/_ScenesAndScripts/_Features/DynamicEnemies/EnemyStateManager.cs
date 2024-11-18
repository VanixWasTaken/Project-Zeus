using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyBaseState roamingState = new EnemyAttackingState();
    public EnemyBaseState chasingState = new EnemyChasingState();
    public EnemyBaseState attackingState = new EnemyAttackingState();

    private EnemyBaseState currentState;

    void Start()
    {
        currentState = roamingState;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState _state)
    {
        currentState = _state;
    }
}
