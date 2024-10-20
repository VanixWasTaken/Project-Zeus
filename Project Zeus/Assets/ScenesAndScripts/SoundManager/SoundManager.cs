using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{

    public Sound[] sounds;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;

            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;

            sound.source.loop = sound.loop;
        }
    }

    public void StartPlaying(string soundName)
    {
        Sound s = Array.Find(sounds, sound  => sound.name == soundName);
        
        if (s == null)
        {
            Debug.LogError("Error! File with the name: " + soundName + " not found!");
            return;
        }
        
        s.source.Play();
    }

    public void StopPlaying(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);

        if (s == null)
        {
            Debug.LogError("Error! Sound: " + soundName + " does not exist!");
            return;
        }

        s.source.Stop();
    }
}
