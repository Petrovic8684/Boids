using UnityEngine;

[RequireComponent(typeof(IBoidComputeHandler))]
public class BoidWeightAdjuster : MonoBehaviour, IBoidWeightAdjuster
{
    [SerializeField] private bool alignEnabled = true;
    [SerializeField] private bool cohesionEnabled = true;
    [SerializeField] private bool separationEnabled = true;

    public void SetAlignWeight(bool enabled) => alignEnabled = enabled;
    public void SetCohesionWeight(bool enabled) => cohesionEnabled = enabled;
    public void SetSeparationWeight(bool enabled) => separationEnabled = enabled;

    private IBoidComputeHandler boidComputeHandler;

    void Awake()
    {
        boidComputeHandler = GetComponent<IBoidComputeHandler>();
    }

    private void Update()
    {
        if (boidComputeHandler == null) return;

        foreach (var boid in boidComputeHandler.AllBoids)
        {
            var ctx = boid.Context;
            ctx.RuntimeAlignWeight = alignEnabled ? 1f : 0f;
            ctx.RuntimeCohesionWeight = cohesionEnabled ? 1f : 0f;
            ctx.RuntimeSeperateWeight = separationEnabled ? 1f : 0f;
        }
    }
}
