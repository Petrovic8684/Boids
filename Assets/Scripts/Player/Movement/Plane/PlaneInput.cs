using UnityEngine;

public class PlaneInput : MonoBehaviour, IInput, IBoostInput
{
    [SerializeField] private KeyCode LeftRollKey = KeyCode.Q;
    [SerializeField] private KeyCode RightRollKey = KeyCode.E;
    [SerializeField] private KeyCode AccelerateKey = KeyCode.LeftShift;

    public float Pitch => Input.GetAxis("Vertical");
    public float Yaw => Input.GetAxis("Horizontal");
    public float Roll => Input.GetKey(LeftRollKey) ? 1 : Input.GetKey(RightRollKey) ? -1 : 0;

    public bool IsBoosting => Input.GetKey(AccelerateKey);
}
