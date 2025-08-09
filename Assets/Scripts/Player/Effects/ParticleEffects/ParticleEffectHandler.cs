using UnityEngine;

public class ParticleEffectHandler : MonoBehaviour, IParticleEffectHandler
{
    [SerializeField] private ParticleSystem effect;
    public ParticleSystem Effect => effect;

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
