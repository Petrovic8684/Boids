using UnityEngine;

public interface IBoidMovement
{
    Vector3 UpdateVelocity(Vector3 acceleration, float deltaTime);
    Vector3 CurrentVelocity { get; }
    Vector3 Direction { get; }
}
