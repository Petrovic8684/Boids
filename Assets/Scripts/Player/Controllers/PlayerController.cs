using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IInput input;
    private IBoostInput boostInput;
    private IMovable movable;
    private IOrientationRotatable rotatable;
    private IBoostable boostable;

    private void Awake()
    {
        input = GetComponent<IInput>();
        boostInput = GetComponent<IBoostInput>();
        movable = GetComponent<IMovable>();
        rotatable = GetComponent<IOrientationRotatable>();
        boostable = GetComponent<IBoostable>();
    }

    private void FixedUpdate()
    {
        movable.Move(transform.forward);
        rotatable.Rotate(input.Pitch, input.Yaw, input.Roll);
        boostable.Boost(boostInput.IsBoosting);
    }
}
