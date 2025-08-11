using UnityEngine;

public interface IBoid
{
    BoidContext Context { get; }
    void UpdatePerception(Vector3 avgFlockHeading, Vector3 centreOfFlockmates, Vector3 avgAvoidanceHeading, int numFlockmates);
    void Simulate(float deltaTime);
}
