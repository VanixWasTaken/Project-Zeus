using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkerStateManager : MonoBehaviour
{
    // All available worker states
    #region WorkerStates
    public WorkerBaseState currentState;
    public WorkerIdleState idleState = new WorkerIdleState();
    public WorkerWalkingState walkingState = new WorkerWalkingState();
    #endregion


    // All References
    #region References
    /*public Animator mAnimator;
    [SerializeField] Camera mainCamera;
    [SerializeField] Vector3 mouseClickPos;
    [SerializeField] CommandCenterStateManager commandCenter;
    public ObjectAudioData audioSheet;
    public NavMeshAgent navMeshAgent;
    [SerializeField] Vector3 targetPosition;
    [SerializeField] GameObject selectionIndicator;
    [SerializeField] GameObject enemyDetector;
    public float life = 100;
    public List<GameObject> enemiesInRange;
    public string myEnemyTag;
    public int damage = 10;
    public bool isDead = false;*/
    #endregion




    void Start()
    {
        //selectionIndicator.SetActive(false);
        //currentState = idleState;
        //currentState.EnterState(this);
    }

   
    void Update()
    {
        //currentState.UpdateState(this);
    }

    public void SwitchStates(UnitBaseState state)
    {
         //state.EnterState(this);
    }


}
