using UnityEngine;

public class PlaneInput : MonoBehaviour, IInput, IBoostInput
{
    [SerializeField] private KeyCode leftRollKey = KeyCode.Q;
    [SerializeField] private KeyCode rightRollKey = KeyCode.E;
    [SerializeField] private KeyCode boostKey = KeyCode.LeftShift;

    public KeyCode LeftRollKey => leftRollKey;
    public KeyCode RightRollKey => rightRollKey;
    public KeyCode BoostKey => boostKey;

    public float Pitch => Input.GetAxis("Vertical");
    public float Yaw => Input.GetAxis("Horizontal");
    public float Roll => Input.GetKey(LeftRollKey) ? 1 : Input.GetKey(RightRollKey) ? -1 : 0;

    public bool IsBoosting => Input.GetKey(BoostKey);
}
