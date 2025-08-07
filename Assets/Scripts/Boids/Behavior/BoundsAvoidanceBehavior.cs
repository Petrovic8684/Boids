using UnityEngine;

public class BoundsAvoidanceBehavior : IFlockingBehavior
{
    private BoxCollider boundsCollider;

    public BoundsAvoidanceBehavior(BoxCollider boundsCollider)
    {
        this.boundsCollider = boundsCollider;
    }

    public Vector3 ComputeAcceleration(BoidContext context)
    {
        if (boundsCollider == null)
            return Vector3.zero;

        Vector3 localPos = boundsCollider.transform.InverseTransformPoint(context.Position);
        Vector3 extents = boundsCollider.size * 0.5f;

        Vector3 force = Vector3.zero;

        if (localPos.x > extents.x - context.Settings.avoidDistance) force.x = -context.Settings.avoidForce;
        else if (localPos.x < -extents.x + context.Settings.avoidDistance) force.x = context.Settings.avoidForce;

        if (localPos.y > extents.y - context.Settings.avoidDistance) force.y = -context.Settings.avoidForce;
        else if (localPos.y < -extents.y + context.Settings.avoidDistance) force.y = context.Settings.avoidForce;

        if (localPos.z > extents.z - context.Settings.avoidDistance) force.z = -context.Settings.avoidForce;
        else if (localPos.z < -extents.z + context.Settings.avoidDistance) force.z = context.Settings.avoidForce;

        if (force == Vector3.zero)
            return Vector3.zero;

        Vector3 desiredVelocity = force.normalized * context.Settings.maxSpeed;
        Vector3 steering = desiredVelocity - context.Velocity;
        return Vector3.ClampMagnitude(steering, context.Settings.maxSteerForce) * context.Settings.avoidCollisionWeight;
    }
}
