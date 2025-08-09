using UnityEngine;

public class AudioEffectHandler : MonoBehaviour, IAudioEffectHandler
{
    [SerializeField] private AudioSource effect;
    public AudioSource Effect => effect;

    public void ToggleEffect(bool criterion)
    {
        if (criterion && !Effect.isPlaying) PlayEffect();
        else if (!criterion && Effect.isPlaying) StopEffect();
    }

    public void PlayEffect()
    {
        Effect.Play();
    }

    public void StopEffect()
    {
        Effect.Stop();
    }
}
