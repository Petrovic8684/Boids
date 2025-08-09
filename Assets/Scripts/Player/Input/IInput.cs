using UnityEngine;

public interface IInput
{
    float Pitch { get; }
    float Yaw { get; }
    float Roll { get; }

    KeyCode LeftRollKey { get; }
    KeyCode RightRollKey { get; }
}
