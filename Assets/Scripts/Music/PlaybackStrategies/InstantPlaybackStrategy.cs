using System;
using UnityEngine;

public class InstantPlaybackStrategy : MonoBehaviour, IAudioPlaybackStrategy
{
    public void PlayFirst(AudioSource audio)
    {
        audio.volume = 1f;
        audio.Play();
    }

    public void PlayNext(AudioSource current, AudioSource next, Action onComplete = null)
    {
        current.Stop();
        next.volume = 1f;
        next.Play();
        onComplete?.Invoke();
    }
}
