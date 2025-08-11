using UnityEngine;

public class AlignmentBehavior : MonoBehaviour, IFlockingBehavior
{
    public Vector3 ComputeAcceleration(BoidContext context)
    {
        if (context.NumFlockmates == 0) return Vector3.zero;
        return SteerTowards(context.AvgFlockHeading, context);
    }

    private Vector3 SteerTowards(Vector3 vector, BoidContext context)
    {
        Vector3 v = vector.normalized * context.Settings.maxSpeed - context.Velocity;
        return Vector3.ClampMagnitude(v, context.Settings.maxSteerForce) * context.RuntimeAlignWeight;
    }
}
