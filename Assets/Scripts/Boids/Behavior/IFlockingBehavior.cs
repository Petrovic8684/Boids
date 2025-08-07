using UnityEngine;

public interface IFlockingBehavior
{
    Vector3 ComputeAcceleration(BoidContext context);
}
