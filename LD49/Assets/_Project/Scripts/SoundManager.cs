﻿using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singleton<SoundManager>
{
    public SoundAudioClip[] soundAudioClipArray;

    [SerializeField] private float volume; // Volume of SFX

    private AudioSource source; // Private reference of the audioSource where we are going to play our SFX

    private Dictionary<SoundType, float> soundTimerDictionary;

    protected void Awake()
    {
        //base.Awake();

        source = GetComponent<AudioSource>();

        Initialize();
    }

    private void Initialize()
    {
        soundTimerDictionary = new Dictionary<SoundType, float>
        {
            [SoundType.PlayerWalk] = 0f
        };
    }

    public void PlaySound(SoundType st)
    {
        if (CanPlaySound(st))
        {
            source.PlayOneShot(SearchSound(st), volume * volume);
        }
    }

    public void PlaySound(SoundType st, float newVolume)
    {
        if (CanPlaySound(st))
        {
            source.PlayOneShot(SearchSound(st), volume * newVolume);
        }
    }

    private bool CanPlaySound(SoundType sound)
    {
        switch(sound)
        {
            default:
                return true;
            case SoundType.PlayerWalk:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .475f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else 
                        return false;
                }
                else 
                    return true;
        }
    }

    private AudioClip SearchSound(SoundType st)
    {
        foreach (SoundAudioClip sac in soundAudioClipArray)
        {
            if (sac.sound == st)
                return sac.audioClip;
        }

        Debug.LogError("Sound Not Found");
        return null;
    }
}

[System.Serializable]
public class SoundAudioClip
{
    public SoundType sound;
    public AudioClip audioClip;
}

public enum SoundType
{
    PlayerRockHit,
    PlayerWalk,
    PlayerJump,
    MouseOverButton,
    ButtonClick,
    Coin,
    PowerUp,
    RockSmash,
    PlayerDead
}
