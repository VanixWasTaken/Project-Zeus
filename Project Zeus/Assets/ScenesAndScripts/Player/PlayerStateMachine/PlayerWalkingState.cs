using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using AudioType = UnityEngine.Audio.AudioType;

public class PlayerWalkingState : PlayerBaseState
{

    float speed = 5f;
    float rotationSpeed = 15f;

    public override void EnterState(PlayerStateManager player)
    {
        player.mAnimator.SetFloat("anSpeed", 1);
    }

    public override void UpdateState(PlayerStateManager player)
    {
        player.transform.position = Vector3.MoveTowards(player.transform.position, player.mouseClickPos, speed * Time.deltaTime);

        if (player.transform.position == player.mouseClickPos)
        {
            player.SwitchStates(player.idleState);
        }

        // Calculate Rotation and apply
        Vector3 direction = player.mouseClickPos - player.transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public override void OnFootstep(PlayerStateManager player)
    {
        player.audioController.RandomizeAudioPitch(AudioType.SMFootstep_01, 0.8f, 1.2f);
    }
}
