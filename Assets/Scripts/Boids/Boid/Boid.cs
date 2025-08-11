using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour, IBoid
{
    private BoidSettings settings;
    public BoidContext Context { get; private set; }
    private IBoidMovement movement;
    private Transform cachedTransform;
    private IReadOnlyList<IFlockingBehavior> behaviors;

    internal void Initialize(
        BoidSettings settings,
        BoidContext context,
        IReadOnlyList<IFlockingBehavior> behaviors,
        IBoidMovement movement)
    {
        this.settings = settings;
        Context = context;
        this.behaviors = behaviors;
        this.movement = movement;
        cachedTransform = transform;
    }

    public void UpdatePerception(
        Vector3 avgFlockHeading,
        Vector3 centreOfFlockmates,
        Vector3 avgAvoidanceHeading,
        int numFlockmates)
    {
        Context.AvgFlockHeading = avgFlockHeading;
        Context.CentreOfFlockmates = centreOfFlockmates;
        Context.AvgAvoidanceHeading = avgAvoidanceHeading;
        Context.NumFlockmates = numFlockmates;
    }

    public void Simulate(float deltaTime)
    {
        Context.Position = cachedTransform.position;
        Context.Forward = cachedTransform.forward;
        Context.Velocity = movement.CurrentVelocity;
        Context.Settings = settings;

        Vector3 acceleration = Vector3.zero;
        foreach (var behavior in behaviors)
            acceleration += behavior.ComputeAcceleration(Context);

        Vector3 velocity = movement.UpdateVelocity(acceleration, deltaTime);
        cachedTransform.position += velocity * deltaTime;
        cachedTransform.forward = movement.Direction;
    }
}
