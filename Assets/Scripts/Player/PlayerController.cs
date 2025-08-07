using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IInput input;
    private IMovable movable;
    private IRotatable rotatable;

    private void Awake()
    {
        input = GetComponent<IInput>();
        movable = GetComponent<IMovable>();
        rotatable = GetComponent<IRotatable>();
    }

    private void FixedUpdate()
    {
        movable.Move(transform.forward);
        rotatable.Rotate(input.Pitch, input.Yaw, input.Roll);
    }
}
