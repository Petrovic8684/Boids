using UnityEngine;

public class PlanePropellerRotation : MonoBehaviour, IAxisRotatable
{
    [SerializeField] private float rotationSpeed = 360f;

    private void Update()
    {
        Rotate(Time.deltaTime);
    }

    public void Rotate(float deltaTime)
    {
        transform.Rotate(0f, 0f, rotationSpeed * deltaTime);
    }
}
