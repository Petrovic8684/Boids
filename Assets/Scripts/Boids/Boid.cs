using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    private BoidSettings settings;
    private BoidMovement movement;
    private BoidContext context;
    private Transform cachedTransform;

    private List<IFlockingBehavior> behaviors;

    public void Initialize(BoidSettings settings, BoidContext context, List<IFlockingBehavior> behaviors)
    {
        this.settings = settings;
        this.context = context;
        this.behaviors = behaviors;
        cachedTransform = transform;
        movement = new BoidMovement(settings, cachedTransform.forward);
    }

    public void UpdatePerception(Vector3 avgFlockHeading, Vector3 centreOfFlockmates, Vector3 avgAvoidanceHeading, int numFlockmates)
    {
        context.AvgFlockHeading = avgFlockHeading;
        context.CentreOfFlockmates = centreOfFlockmates;
        context.AvgAvoidanceHeading = avgAvoidanceHeading;
        context.NumFlockmates = numFlockmates;
    }

    public void Tick()
    {
        Vector3 position = cachedTransform.position;
        Vector3 forward = cachedTransform.forward;

        context.Position = position;
        context.Forward = forward;
        context.Velocity = movement.CurrentVelocity;
        context.Settings = settings;

        Vector3 acceleration = Vector3.zero;
        foreach (var behavior in behaviors)
        {
            acceleration += behavior.ComputeAcceleration(context);
        }

        Vector3 velocity = movement.UpdateVelocity(acceleration, Time.deltaTime);
        Vector3 dir = movement.Direction;

        cachedTransform.position += velocity * Time.deltaTime;
        cachedTransform.forward = dir;
    }
}
