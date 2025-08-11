using System;
using UnityEngine;

public interface IAudioPlaybackStrategy
{
    void PlayFirst(AudioSource audio);
    void PlayNext(AudioSource current, AudioSource next, Action onComplete = null);
}
