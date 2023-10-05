using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudioManager : MonoBehaviour
{
    public static PlayerAudioManager Script;

    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _walkAudioSource;
    [SerializeField] private Sounds[] _sounds;

    private void Awake()
    {
        Script = this;
    }

    public void PlaySound(Sounds.Sound type)
    {
        int index = (int)type;
        Play(index);
    }
    public void PlaySound(int index)
    {
        Play(index);
    }

    public void SetActiveWalkSound(bool isActive)
    {
        if (isActive && Values.CanMove) _walkAudioSource.Play();
        else _walkAudioSource.Stop();
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    private void Play(int index)
    {
        _audioSource.clip = _sounds[index].sound;
        _audioSource.volume = _sounds[index].volume;
        _audioSource.loop = _sounds[index].isLoop;
        _audioSource.Play();
    }
}
