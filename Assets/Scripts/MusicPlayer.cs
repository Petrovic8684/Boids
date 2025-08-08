using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance { get; private set; }

    [SerializeField] private List<AudioSource> audioSources;
    [SerializeField] private float fadeDuration = 1.5f;

    private int currentIndex = 0;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        if (audioSources == null || audioSources.Count == 0)
        {
            enabled = false;
            return;
        }

        foreach (var audioSource in audioSources)
        {
            audioSource.volume = 0f;
            audioSource.Stop();
        }

        PlayCurrent();
    }

    private void Update()
    {
        if (audioSources.Count == 0) return;

        var currentAudio = audioSources[currentIndex];

        if (currentAudio.isPlaying && currentAudio.time < currentAudio.clip.length - fadeDuration)
            return;

        if (fadeCoroutine == null)
        {
            int nextIndex = (currentIndex + 1) % audioSources.Count;
            fadeCoroutine = StartCoroutine(FadeToNextTrack(nextIndex));
        }
    }

    private void PlayCurrent()
    {
        var audio = audioSources[currentIndex];
        audio.volume = 0f;
        audio.Play();
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
            fadeCoroutine = null;
        }
        fadeCoroutine = StartCoroutine(FadeIn(audio));
    }

    private IEnumerator FadeToNextTrack(int nextIndex)
    {
        var currentAudio = audioSources[currentIndex];
        var nextAudio = audioSources[nextIndex];

        nextAudio.volume = 0f;
        nextAudio.Play();

        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float t = timer / fadeDuration;

            currentAudio.volume = Mathf.Lerp(1f, 0f, t);
            nextAudio.volume = Mathf.Lerp(0f, 1f, t);

            yield return null;
        }

        currentAudio.Stop();
        currentAudio.volume = 0f;

        currentIndex = nextIndex;

        fadeCoroutine = null;
    }

    private IEnumerator FadeIn(AudioSource audio)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            audio.volume = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        audio.volume = 1f;
        fadeCoroutine = null;
    }
}
