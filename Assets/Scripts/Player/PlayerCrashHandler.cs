using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCrashHandler : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private AudioSource crashAudio;
    [SerializeField] private ParticleSystem crashEffect;
    [SerializeField] private GameObject playerVisual;
    [SerializeField] private float restartDelay = 3f;

    public static Action OnCrashed;
    private bool isCrashed = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (isCrashed) return;

        if (((1 << collision.gameObject.layer) & obstacleLayer.value) != 0)
            HandleCrash();
    }

    private void HandleCrash()
    {
        isCrashed = true;
        OnCrashed?.Invoke();

        if (crashAudio != null)
            crashAudio.Play();

        if (crashEffect != null)
            crashEffect.Play();

        if (playerVisual != null)
            playerVisual.SetActive(false);

        var movement = GetComponentInParent<PlayerController>();
        if (movement != null)
            movement.enabled = false;

        Invoke(nameof(RestartScene), restartDelay);
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
