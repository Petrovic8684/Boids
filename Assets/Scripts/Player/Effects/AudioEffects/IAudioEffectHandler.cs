using UnityEngine;

public interface IAudioEffectHandler : IEffectHandler
{
    AudioSource Effect { get; }
}
