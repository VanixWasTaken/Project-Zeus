using System;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Audio/Player", fileName = "AudioSheet")]
public class NewFMODAudioData : ScriptableObject
{
    public enum ObjectType
    {
        Worker,
        Recon,
        Fighter,

        Screamer,

        Ambience,

        Music,

        UI
    }

    public ObjectType objectType;


}
