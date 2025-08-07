using UnityEngine;

public class CollisionAvoidanceBehavior : IFlockingBehavior
{
    public Vector3 ComputeAcceleration(BoidContext context)
    {
        if (!IsHeadingForCollision(context)) return Vector3.zero;

        Vector3 dir = FindCollisionFreeDirection(context);
        Vector3 v = dir.normalized * context.Settings.maxSpeed - context.Velocity;
        return Vector3.ClampMagnitude(v, context.Settings.maxSteerForce) * context.Settings.avoidCollisionWeight;
    }

    private bool IsHeadingForCollision(BoidContext context)
    {
        return Physics.SphereCast(context.Position, context.Settings.boundsRadius, context.Forward, out _, context.Settings.collisionAvoidDst, context.Settings.obstacleMask);
    }

    private Vector3 FindCollisionFreeDirection(BoidContext context)
    {
        foreach (var dir in BoidHelper.directions)
        {
            Vector3 worldDir = Quaternion.LookRotation(context.Forward) * dir;
            if (!Physics.SphereCast(context.Position, context.Settings.boundsRadius, worldDir, out _, context.Settings.collisionAvoidDst, context.Settings.obstacleMask))
            {
                return worldDir;
            }
        }

        return context.Forward;
    }
}