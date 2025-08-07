using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlaneMovement : MonoBehaviour, IMovable, IRotatable
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float rollStabilizeSpeed = 2f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    public void Rotate(float pitch, float yaw, float rollInput)
    {
        Quaternion currentRotation = rb.rotation;

        // Kreiraj ciljnu rotaciju na osnovu inputa pitch i yaw
        Quaternion pitchYawRotation = Quaternion.Euler(pitch * rotationSpeed * Time.deltaTime,
                                                       yaw * rotationSpeed * Time.deltaTime,
                                                       0f); // roll stavljen na 0, ide stabilizacija posebno

        Quaternion targetRotation = currentRotation * pitchYawRotation;

        // Izračunaj trenutni roll ugao u lokalnom prostoru (relativno na globalni horizont)
        float currentRoll = NormalizeAngle(targetRotation.eulerAngles.z);

        float desiredRoll = 0f;

        if (!Mathf.Approximately(rollInput, 0f))
        {
            // Ako imaš input za roll, inkrementiraj roll ugao proporcionalno
            desiredRoll = currentRoll + (-rollInput * rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Ako nema inputa, stabilizuj roll ka 0 glatko lerpujući
            desiredRoll = Mathf.LerpAngle(currentRoll, 0f, rollStabilizeSpeed * Time.deltaTime);
        }

        // Kreiraj rotaciju sa željenim roll-om, ali pitch i yaw već urađeni u targetRotation
        // Zato prvo izvući pitch i yaw iz targetRotation (tj. pitchYawRotation) i dodati roll odvojeno

        Vector3 targetEuler = targetRotation.eulerAngles;
        targetEuler.z = desiredRoll;

        Quaternion finalRotation = Quaternion.Euler(targetEuler);

        rb.MoveRotation(finalRotation);
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;
        return angle;
    }
}
