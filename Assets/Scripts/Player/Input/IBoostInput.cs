using UnityEngine;

public interface IBoostInput
{
    bool IsBoosting { get; }

    KeyCode BoostKey { get; }
}
