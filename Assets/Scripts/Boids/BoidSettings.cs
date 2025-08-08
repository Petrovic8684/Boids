using UnityEngine;

[CreateAssetMenu]
public class BoidSettings : ScriptableObject
{
    public float minSpeed = 2;
    public float maxSpeed = 5;
    public float perceptionRadius = 3f;
    public float avoidanceRadius = 1.2f;
    public float maxSteerForce = 3;

    public float alignWeight = 1;
    public float cohesionWeight = 1;
    public float seperateWeight = 1;
    public float targetWeight = 0;

    [Header("Collisions")]
    public LayerMask obstacleMask;
    public float boundsRadius = 0.4f;
    public float avoidCollisionWeight = 10;
    public float collisionAvoidDst = 5;
    public float avoidDistance = 8;
    public float avoidForce = 5;
}
