using UnityEngine;

public class CameraFollowBasic : MonoBehaviour, ICameraFollow
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    public void Follow(Camera camera, Transform target, float deltaTime)
    {
        Vector3 targetEuler = target.eulerAngles;
        Quaternion yawRotation = Quaternion.Euler(0, targetEuler.y, 0);

        Vector3 desiredPosition = target.position + yawRotation * offset;
        camera.transform.position = Vector3.Lerp(camera.transform.position, desiredPosition, followSpeed * deltaTime);

        Vector3 lookTarget = new Vector3(target.position.x, camera.transform.position.y, target.position.z);
        Vector3 lookDirection = lookTarget - camera.transform.position;

        if (lookDirection.sqrMagnitude > 0.001f)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(lookDirection);
            camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, desiredRotation, rotationSpeed * deltaTime);
        }
    }
}
