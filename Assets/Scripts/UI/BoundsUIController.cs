using UnityEngine;

public class BoundsUIController : MonoBehaviour
{
    [SerializeField] private MeshRenderer bounds;

    public void SetDrawBounds(bool enabled)
    {
        bounds.enabled = enabled;
    }
}
