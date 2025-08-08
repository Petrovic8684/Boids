using UnityEngine;

public class CohesionBehavior : IFlockingBehavior
{
    public Vector3 ComputeAcceleration(BoidContext context)
    {
        if (context.NumFlockmates == 0) return Vector3.zero;
        Vector3 offset = context.CentreOfFlockmates - context.Position;
        return SteerTowards(offset, context);
    }

    private Vector3 SteerTowards(Vector3 vector, BoidContext context)
    {
        Vector3 v = vector.normalized * context.Settings.maxSpeed - context.Velocity;
        return Vector3.ClampMagnitude(v, context.Settings.maxSteerForce) * context.RuntimeCohesionWeight;
    }
}
