using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlaneMovement : MonoBehaviour, IMovable, IRotatable, IBoostable
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float rollStabilizeSpeed = 2f;
    [SerializeField] private float boostMultiplier = 1.8f;
    [SerializeField] private ParticleSystem boostEffect;

    private Rigidbody rb;

    void OnEnable()
    {
        PlayerCrashHandler.OnCrashed += DisableBoostEffect;
    }

    void OnDisable()
    {
        PlayerCrashHandler.OnCrashed -= DisableBoostEffect;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boostEffect.gameObject.SetActive(true);
        boostEffect.Stop();
    }

    public void Move(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    public void Rotate(float pitch, float yaw, float rollInput)
    {
        Quaternion currentRotation = rb.rotation;
        Quaternion pitchYawRotation = Quaternion.Euler(pitch * rotationSpeed * Time.deltaTime, yaw * rotationSpeed * Time.deltaTime, 0f);
        Quaternion targetRotation = currentRotation * pitchYawRotation;

        float currentRoll = NormalizeAngle(targetRotation.eulerAngles.z);

        float desiredRoll = 0f;
        if (!Mathf.Approximately(rollInput, 0f))
            desiredRoll = currentRoll + (-rollInput * rotationSpeed * Time.deltaTime);
        else desiredRoll = Mathf.LerpAngle(currentRoll, 0f, rollStabilizeSpeed * Time.deltaTime);

        Vector3 targetEuler = targetRotation.eulerAngles;
        targetEuler.z = desiredRoll;

        Quaternion finalRotation = Quaternion.Euler(targetEuler);
        rb.MoveRotation(finalRotation);
    }

    public void Boost(bool isBoosting)
    {
        rb.velocity *= isBoosting ? boostMultiplier : 1;

        if (boostEffect == null)
            return;

        if (isBoosting && !boostEffect.isPlaying)
            boostEffect.Play();
        else if (!isBoosting && boostEffect.isPlaying)
            boostEffect.Stop();
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;
        return angle;
    }

    private void DisableBoostEffect()
    {
        boostEffect.Stop();
        boostEffect.gameObject.SetActive(false);
    }
}
