using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] SoundType[] soundtypes;

    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (SoundType s in soundtypes)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();

            s.audioSource.clip = s.audioClip;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
            s.audioSource.playOnAwake = s.playOnAwake;
            s.audioSource.loop = s.loop;
            s.audioSource.priority = s.priority;
        }
    }

    private void Start()
    {
        Play(Sounds.ConstantBackGround);
    }
    public void Play(Sounds sName)
    {
        SoundType s = Array.Find(soundtypes, sound => sound.name == sName);
        if (s == null)
        {
            Debug.Log("Sound clip is not available: " + sName);
            return;
        }
        if (!s.audioSource.isPlaying)
        {
            s.audioSource.Play();
        }
        

    }

}
