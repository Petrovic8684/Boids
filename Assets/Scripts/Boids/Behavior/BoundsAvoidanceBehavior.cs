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
        float avoidDist = context.Settings.avoidDistance;

        Vector3 toCenterLocal = -localPos;

        float distFactor = 0f;

        float distX = Mathf.Min(extents.x - localPos.x, localPos.x + extents.x);
        float distY = Mathf.Min(extents.y - localPos.y, localPos.y + extents.y);
        float distZ = Mathf.Min(extents.z - localPos.z, localPos.z + extents.z);

        distFactor = Mathf.Min(distX, Mathf.Min(distY, distZ));

        float t = Mathf.InverseLerp(avoidDist, 0f, distFactor);
        t = Mathf.Clamp01(t);

        Vector3 forceLocal = toCenterLocal.normalized * t;

        Vector3 forceWorld = boundsCollider.transform.TransformDirection(forceLocal);

        Vector3 desiredVelocity = forceWorld.normalized * context.Settings.maxSpeed;
        Vector3 steering = desiredVelocity - context.Velocity;

        return Vector3.ClampMagnitude(steering, context.Settings.maxSteerForce) * context.Settings.avoidForce;
    }
}
