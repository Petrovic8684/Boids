using UnityEngine;

public class GameObjectToggler : MonoBehaviour, IToggler
{
    [SerializeField] private GameObject target;

    private void OnEnable()
    {
        PlayerCrashController.OnPlayerCrashed += DisableTarget;
    }

    private void OnDisable()
    {
        PlayerCrashController.OnPlayerCrashed -= DisableTarget;
    }

    public void EnableTarget()
    {
        target.SetActive(true);
    }

    public void DisableTarget()
    {
        target.SetActive(false);
    }
}
