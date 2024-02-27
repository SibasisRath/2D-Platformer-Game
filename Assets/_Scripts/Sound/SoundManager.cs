using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundType[] soundtypes;

    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < soundtypes.Length; i++)
        {
            soundtypes[i].audioSource = gameObject.AddComponent<AudioSource>();

            soundtypes[i].audioSource.clip = soundtypes[i].audioClip;
            soundtypes[i].audioSource.volume = soundtypes[i].volume;
            soundtypes[i].audioSource.pitch = soundtypes[i].pitch;
            soundtypes[i].audioSource.playOnAwake = soundtypes[i].playOnAwake;
            soundtypes[i].audioSource.loop = soundtypes[i].loop;
            soundtypes[i].audioSource.priority = soundtypes[i].priority;
        }
    }

    private void Start()
    {
        Play(Sounds.ConstantBackGround);
    }
    public void Play(Sounds sName) //it is an enum.
    {
        SoundType s = Array.Find(soundtypes, sound => sound.name == sName);
        if (s == null)
        {
            return;
        }
        if (!s.audioSource.isPlaying)
        {
            s.audioSource.Play();
        }
    }
}
