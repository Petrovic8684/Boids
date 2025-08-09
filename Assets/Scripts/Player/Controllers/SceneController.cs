using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private float restartDelay = 3f;

    private void OnEnable()
    {
        PlayerCrashController.OnPlayerCrashed += RestartSceneWithDelay;
    }

    private void OnDisable()
    {
        PlayerCrashController.OnPlayerCrashed -= RestartSceneWithDelay;
    }

    private void RestartSceneWithDelay()
    {
        StartCoroutine(RestartSceneCoroutine());
    }

    private System.Collections.IEnumerator RestartSceneCoroutine()
    {
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
