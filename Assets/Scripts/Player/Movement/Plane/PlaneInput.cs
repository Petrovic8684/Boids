using UnityEngine;

public class PlaneInput : MonoBehaviour, IInput
{
    [SerializeField] private KeyCode LeftRollKey = KeyCode.Q;
    [SerializeField] private KeyCode RightRollKey = KeyCode.E;

    public float Pitch => Input.GetAxis("Vertical");
    public float Yaw => Input.GetAxis("Horizontal");
    public float Roll => Input.GetKey(LeftRollKey) ? 1 : Input.GetKey(RightRollKey) ? -1 : 0;
}
