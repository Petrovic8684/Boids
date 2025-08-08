using UnityEngine;

public class CameraFovController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float normalFov = 60f;
    [SerializeField] private float boostFov = 75f;
    [SerializeField] private float fovTransitionSpeed = 5f;

    private Camera cam;
    private IBoostInput boostInput;

    private void Awake()
    {
        enabled = true;
        cam = Camera.main;
    }

    void OnEnable()
    {
        PlayerCrashHandler.OnCrashed += Disable;
    }

    void OnDisable()
    {
        PlayerCrashHandler.OnCrashed -= Disable;
    }

    private void Start()
    {
        if (target == null)
            return;

        boostInput = target.GetComponentInParent<IBoostInput>();
    }

    private void Update()
    {
        if (cam == null || target == null || boostInput == null) return;

        float desiredFov = (boostInput != null && boostInput.IsBoosting) ? boostFov : normalFov;
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, desiredFov, fovTransitionSpeed * Time.deltaTime);
    }

    private void Disable()
    {
        enabled = false;
    }
}
