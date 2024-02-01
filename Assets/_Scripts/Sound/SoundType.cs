using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundType
{
    public Sounds name;

    public AudioClip audioClip;

    [HideInInspector]
    public AudioSource audioSource;

    [Range(0f,1f)]
    public float volume;
    [Range(0.1f, 3.1f)]
    public float pitch;
    public bool playOnAwake;
    public bool loop;
    [Range(0, 256)]
    public int priority;
}
