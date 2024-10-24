using System;
using System.Collections;
using System.Collections.Generic;
using UnityCore.Audio;
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
    public AudioController audioController;
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
        PlayFootsteps();
    }

    private void PlayFootsteps()
    {
        int rando = Randomizer(1, 4);

        if (rando == 1)
        {
            audioController.PlayAudio(UnityCore.Audio.AudioType.SMFootstep_01);
        }
        else if (rando == 2)
        {
            audioController.PlayAudio(UnityCore.Audio.AudioType.SMFootstep_02);
        }
        else if (rando == 3)
        {
            audioController.PlayAudio(UnityCore.Audio.AudioType.SMFootstep_03);
        }
    }

    public int Randomizer(int min, int max)
    {
        System.Random rnd = new System.Random();
        int num = rnd.Next(min, max);
        return num;
    }
}
