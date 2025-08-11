using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private new Camera camera;
    [SerializeField] private Transform target;

    private ICameraFollow cameraFollow;
    private ICameraViewHandler cameraViewHandler;

    private void Awake()
    {
        cameraFollow = GetComponent<ICameraFollow>();
        cameraViewHandler = GetComponent<ICameraViewHandler>();
    }

    private void LateUpdate()
    {
        float dt = Time.deltaTime;

        cameraFollow.Follow(camera, target, dt);
        cameraViewHandler.UpdateView(camera, target, dt);
    }
}
