using System;
using FMODUnity;
using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "SO/Audio/Player", fileName = "Unit Sheet")]
public class FMODAudioData : ScriptableObject
{
    public enum SoundID
    {
        // SFX Reference
        UnitPoweringUp,
        UnitPoweringDown,

        UnitReconFootstep,

        // VO Reference
        BARK01,
        BARK02,
        BARK03
    }

    
    [Serializable]
    public class NamedSFX
    {
        public SoundID eventID;
        public EventReference Event;
    }

    // this is used to reference Events for the sfx via code in the player
    [Header("SFX")]
    public List<NamedSFX> SFXList;

    public EventReference GetSFXByName(SoundID _eventID)
    {
        foreach (var sfx in SFXList)
        {
            if (sfx.eventID == _eventID)
            {
                return sfx.Event;
            }
        }
        Debug.LogWarning($"No SFX found with name {_eventID}!");
        return default;
    }
}