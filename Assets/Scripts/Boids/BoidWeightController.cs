using UnityEngine;

[RequireComponent(typeof(BoidManager))]
public class BoidWeightController : MonoBehaviour, IBoidWeightController
{
    private BoidManager boidManager;

    [Range(0f, 1f)] public float alignWeight = 1f;
    [Range(0f, 1f)] public float cohesionWeight = 1f;
    [Range(0f, 1f)] public float separationWeight = 1f;

    void Awake()
    {
        boidManager = GetComponent<BoidManager>();
    }

    void Update()
    {
        if (boidManager == null || boidManager.Boids == null) return;

        foreach (var boid in boidManager.Boids)
        {
            if (boid.Context != null)
            {
                boid.Context.RuntimeAlignWeight = alignWeight;
                boid.Context.RuntimeCohesionWeight = cohesionWeight;
                boid.Context.RuntimeSeperateWeight = separationWeight;
            }
        }
    }

    public void SetAlignWeight(bool enabled)
    {
        alignWeight = enabled ? 1f : 0f;
    }
    public void SetCohesionWeight(bool enabled)
    {
        cohesionWeight = enabled ? 1f : 0f;
    }
    public void SetSeparationWeight(bool enabled)
    {
        separationWeight = enabled ? 1f : 0f;
    }
}
