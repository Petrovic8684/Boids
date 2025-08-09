using System;
using UnityEngine;

public class PlayerCrashController : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;

    public static event Action OnPlayerCrashed;

    private ICrashParticleEffectHandler crashEffectHandler;
    private ICrashAudioEffectHandler crashAudioHandler;

    void Awake()
    {
        crashEffectHandler = GetComponent<ICrashParticleEffectHandler>();
        crashAudioHandler = GetComponent<ICrashAudioEffectHandler>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((obstacleLayer.value & (1 << collision.gameObject.layer)) == 0)
            return;

        HandleCrash();
    }

    private void HandleCrash()
    {
        OnPlayerCrashed?.Invoke();

        crashEffectHandler.PlayEffect();
        crashAudioHandler.PlayEffect();
    }
}
