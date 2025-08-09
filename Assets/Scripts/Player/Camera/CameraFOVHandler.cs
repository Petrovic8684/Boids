using UnityEngine;

public class CameraFovHandler : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float normalFov = 60f;
    [SerializeField] private float zoomInFov = 75f;
    [SerializeField] private float fovTransitionSpeed = 5f;

    private new Camera camera;
    private IBoostInput boostInput;

    private void Awake()
    {
        camera = GetComponent<Camera>();
        boostInput = target.GetComponentInParent<IBoostInput>();
    }

    private void Update()
    {
        float desiredFov = (boostInput != null && boostInput.IsBoosting) ? zoomInFov : normalFov;
        camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, desiredFov, fovTransitionSpeed * Time.deltaTime);
    }
}
