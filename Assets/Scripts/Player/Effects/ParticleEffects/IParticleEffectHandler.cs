using UnityEngine;

public interface IParticleEffectHandler : IEffectHandler
{
    ParticleSystem Effect { get; }
}