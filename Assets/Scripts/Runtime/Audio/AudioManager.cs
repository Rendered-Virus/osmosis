using System;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class AudioManager : Singleton<AudioManager>
{
    
    private AudioSource _sfxSource;
    private AudioSource _musicSource;

    protected override void Awake()
    {
        base.Awake();
        
        if(_sfxSource == null)
            _sfxSource = gameObject.AddComponent<AudioSource>();
        if(_musicSource == null)
            _musicSource = gameObject.AddComponent<AudioSource>();
    }
    
    public void PlaySfx(AudioClip clip, float volume){
        _sfxSource.volume = volume;
        _sfxSource.pitch = 1;
        _sfxSource.PlayOneShot(clip);
    }
    public void PlaySfxWithPitchShift(AudioClip clip,float pitchShift, float volume)
    {
        var pitch = 1 + Random.Range(-pitchShift, pitchShift);
        _sfxSource.volume = volume;
        _sfxSource.pitch = pitch;
        _sfxSource.PlayOneShot(clip);
    }
    public void PlaySfxAtPitch(AudioClip clip,float pitch, float volume)
    {
        _sfxSource.volume = volume;
        _sfxSource.pitch = pitch;
        _sfxSource.PlayOneShot(clip);
    }
    public void PlayMusic(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }
    
}