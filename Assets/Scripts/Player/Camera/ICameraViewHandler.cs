using UnityEngine;

public interface ICameraViewHandler
{
    void UpdateView(Camera camera, Transform target, float deltaTime);
}
