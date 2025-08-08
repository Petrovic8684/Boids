using UnityEngine;

public class FloatingMotion : MonoBehaviour
{
    [SerializeField] private float amplitude = 0.5f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private bool randomizePhase = true;

    private float phaseOffset;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
        phaseOffset = randomizePhase ? Random.Range(0f, Mathf.PI * 2f) : 0f;
    }

    private void Update()
    {
        float newY = startPosition.y + Mathf.Sin((Time.time * frequency) + phaseOffset) * amplitude;
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
