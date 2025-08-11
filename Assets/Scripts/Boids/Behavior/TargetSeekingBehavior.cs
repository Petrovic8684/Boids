using UnityEngine;

public class TargetSeekingBehavior : MonoBehaviour, IFlockingBehavior
{
    [SerializeField] private Transform target;

    public Vector3 ComputeAcceleration(BoidContext context)
    {
        if (target == null) return Vector3.zero;
        Vector3 offset = target.position - context.Position;
        return SteerTowards(offset, context);
    }

    private Vector3 SteerTowards(Vector3 vector, BoidContext context)
    {
        Vector3 v = vector.normalized * context.Settings.maxSpeed - context.Velocity;
        return Vector3.ClampMagnitude(v, context.Settings.maxSteerForce) * context.Settings.targetWeight;
    }
}