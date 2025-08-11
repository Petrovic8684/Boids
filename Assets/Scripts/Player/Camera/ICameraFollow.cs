using UnityEngine;

public interface ICameraFollow
{
    void Follow(Camera camera, Transform target, float deltaTime);
}
