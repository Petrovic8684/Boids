using UnityEngine;

public class BehaviourToggler : MonoBehaviour, IToggler
{
    [SerializeField] private Behaviour target;

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
        target.enabled = true;
    }

    public void DisableTarget()
    {
        target.enabled = false;
    }
}
