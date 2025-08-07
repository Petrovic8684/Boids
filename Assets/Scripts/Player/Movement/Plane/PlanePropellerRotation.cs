using UnityEngine;

public class PlanePropellerRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360f;

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}
