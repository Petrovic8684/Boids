using UnityEngine;
using System;
using System.Collections;

public class FadePlaybackStrategy : MonoBehaviour, IAudioPlaybackStrategy
{
    [SerializeField] private float fadeDuration = 1.5f;

    public void PlayFirst(AudioSource audio)
    {
        StartCoroutine(FadeIn(audio));
    }

    public void PlayNext(AudioSource current, AudioSource next, Action onComplete = null)
    {
        StartCoroutine(FadeToNext(current, next, onComplete));
    }

    private IEnumerator FadeIn(AudioSource audio)
    {
        audio.volume = 0f;
        audio.Play();
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            audio.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        audio.volume = 1f;
    }

    private IEnumerator FadeToNext(AudioSource current, AudioSource next, Action onComplete)
    {
        next.volume = 0f;
        next.Play();
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            current.volume = Mathf.Lerp(1f, 0f, t / fadeDuration);
            next.volume = Mathf.Lerp(0f, 1f, t / fadeDuration);
            yield return null;
        }
        current.Stop();
        onComplete?.Invoke();
    }
}
