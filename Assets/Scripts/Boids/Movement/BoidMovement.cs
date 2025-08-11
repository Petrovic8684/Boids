using UnityEngine;

public class BoidMovement : IBoidMovement
{
    private Vector3 velocity;
    private readonly BoidSettings settings;

    public BoidMovement(BoidSettings settings, Vector3 initialDirection)
    {
        this.settings = settings;
        float startSpeed = (settings.minSpeed + settings.maxSpeed) / 2f;
        velocity = initialDirection * startSpeed;
    }

    public Vector3 UpdateVelocity(Vector3 acceleration, float deltaTime)
    {
        velocity += acceleration * deltaTime;
        float speed = velocity.magnitude;
        Vector3 dir = velocity / speed;
        speed = Mathf.Clamp(speed, settings.minSpeed, settings.maxSpeed);
        velocity = dir * speed;
        return velocity;
    }

    public Vector3 CurrentVelocity => velocity;
    public Vector3 Direction => velocity.normalized;
}
