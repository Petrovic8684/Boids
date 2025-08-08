using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IInput input;
    private IBoostInput boostInput;
    private IMovable movable;
    private IRotatable rotatable;
    private IBoostable boostable;

    private void Awake()
    {
        input = GetComponent<IInput>();
        boostInput = GetComponent<IBoostInput>();
        movable = GetComponent<IMovable>();
        rotatable = GetComponent<IRotatable>();
        boostable = GetComponent<IBoostable>();
    }

    private void FixedUpdate()
    {
        movable.Move(transform.forward);
        rotatable.Rotate(input.Pitch, input.Yaw, input.Roll);
        if (boostInput != null) boostable.Boost(boostInput.IsBoosting);
    }
}
