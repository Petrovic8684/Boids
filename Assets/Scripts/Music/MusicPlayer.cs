using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(IAudioPlaybackStrategy))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioSource> audioSources;
    private IAudioPlaybackStrategy playbackStrategy;
    private int currentIndex = 0;

    private void Awake()
    {
        playbackStrategy = GetComponent<IAudioPlaybackStrategy>();
    }

    private void Start()
    {
        foreach (var audio in audioSources)
        {
            audio.volume = 0f;
            audio.Stop();
        }

        PlayCurrent();
    }

    private void Update()
    {
        var current = audioSources[currentIndex];
        if (current.isPlaying && current.time < current.clip.length - 0.1f)
            return;

        PlayNext((currentIndex + 1) % audioSources.Count);
    }

    private void PlayCurrent()
    {
        playbackStrategy.PlayFirst(audioSources[currentIndex]);
    }

    private void PlayNext(int nextIndex)
    {
        playbackStrategy.PlayNext(audioSources[currentIndex], audioSources[nextIndex],
            () => currentIndex = nextIndex);
    }
}
