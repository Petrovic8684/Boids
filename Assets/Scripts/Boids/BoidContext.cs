using UnityEngine;

public class BoidContext
{
    public Vector3 Position;
    public Vector3 Forward;
    public Vector3 Velocity;
    public BoidSettings Settings;

    public Vector3 AvgFlockHeading;
    public Vector3 CentreOfFlockmates;
    public Vector3 AvgAvoidanceHeading;
    public int NumFlockmates;
}
