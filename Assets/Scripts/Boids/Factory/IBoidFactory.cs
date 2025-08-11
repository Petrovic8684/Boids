using UnityEngine;

public interface IBoidFactory
{
    IBoid CreateBoid(Vector3 position, Transform parentOverride = null);
}
