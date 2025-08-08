using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    private BoidSettings settings;
    private BoidMovement movement;
    internal BoidContext Context { get; set; }
    private Transform cachedTransform;

    private List<IFlockingBehavior> behaviors;

    public void Initialize(BoidSettings settings, BoidContext context, List<IFlockingBehavior> behaviors)
    {
        this.settings = settings;

        Context = context;
        Context.RuntimeAlignWeight = settings.alignWeight;
        Context.RuntimeCohesionWeight = settings.cohesionWeight;
        Context.RuntimeSeperateWeight = settings.seperateWeight;

        this.behaviors = behaviors;
        cachedTransform = transform;
        movement = new BoidMovement(settings, cachedTransform.forward);
    }

    public void UpdatePerception(Vector3 avgFlockHeading, Vector3 centreOfFlockmates, Vector3 avgAvoidanceHeading, int numFlockmates)
    {
        Context.AvgFlockHeading = avgFlockHeading;
        Context.CentreOfFlockmates = centreOfFlockmates;
        Context.AvgAvoidanceHeading = avgAvoidanceHeading;
        Context.NumFlockmates = numFlockmates;
    }

    public void Tick()
    {
        Vector3 position = cachedTransform.position;
        Vector3 forward = cachedTransform.forward;

        Context.Position = position;
        Context.Forward = forward;
        Context.Velocity = movement.CurrentVelocity;
        Context.Settings = settings;

        Vector3 acceleration = Vector3.zero;
        foreach (var behavior in behaviors)
        {
            acceleration += behavior.ComputeAcceleration(Context);
        }

        Vector3 velocity = movement.UpdateVelocity(acceleration, Time.deltaTime);
        Vector3 dir = movement.Direction;

        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = dir;
    }
}
