using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        RandomizeAudio(player, UnityCore.Audio.AudioType.SMFootstep_01, UnityCore.Audio.AudioType.SMFootstep_03);
    }

    private void RandomizeAudio(PlayerStateManager player, UnityCore.Audio.AudioType min, UnityCore.Audio.AudioType max)
    {
        int minValue = (int)min;
        int maxValue = (int)max;

        int rng = Random.Range(minValue, maxValue);

        UnityCore.Audio.AudioType selectedClip = (UnityCore.Audio.AudioType)rng;

        player.audioController.PlayAudio(selectedClip);
        
    }
}
