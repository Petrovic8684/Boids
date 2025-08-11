using UnityEngine;

public class BoundsUIAdjuster : MonoBehaviour
{
    [SerializeField] private MeshRenderer bounds;

    public void SetDrawBounds(bool enabled)
    {
        bounds.enabled = enabled;
    }
}
