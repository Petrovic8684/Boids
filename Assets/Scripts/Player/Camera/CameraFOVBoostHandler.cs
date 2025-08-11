using UnityEngine;

public class CameraFovBoostHandler : MonoBehaviour, ICameraViewHandler
{
    [SerializeField] private float normalFov = 60f;
    [SerializeField] private float zoomInFov = 75f;
    [SerializeField] private float fovTransitionSpeed = 5f;

    private IBoostInput boostInput;

    public void UpdateView(Camera camera, Transform target, float deltaTime)
    {
        if (boostInput == null && target != null)
            boostInput = target.GetComponentInParent<IBoostInput>();

        float desiredFov = (boostInput != null && boostInput.IsBoosting) ? zoomInFov : normalFov;
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, desiredFov, fovTransitionSpeed * deltaTime);
    }
}
