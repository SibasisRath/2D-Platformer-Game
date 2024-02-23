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
