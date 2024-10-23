using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    // All available PlayerStates
    #region PlayerStates
    PlayerBaseState currentState;
    public PlayerIdleState idleState = new PlayerIdleState();
    public PlayerWalkingState walkingState = new PlayerWalkingState();
    #endregion

    // All References
    #region References
    public Animator mAnimator;
    public Camera mainCamera;
    public Vector3 mouseClickPos;
    public SoundManager soundManager;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }


    public void SwitchStates(PlayerBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void OnFootstep()
    {
        return;
    }
}
