using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[CreateAssetMenu(menuName = "SO/Audio/Object", fileName = "New Object Sheet")]
public class ObjectAudioData : ScriptableObject
{
    [Header("Example Header")]
    public EventReference exampleReference01;

    public EventReference exampleReference02;
}
