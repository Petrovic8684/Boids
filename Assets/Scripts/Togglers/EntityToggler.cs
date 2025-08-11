using System.Collections.Generic;
using UnityEngine;

public class EntityToggler : MonoBehaviour, IEntityToggler
{
    [SerializeField] private List<GameObject> gameObjects = new();
    [SerializeField] private List<Behaviour> behaviours = new();

    public void EnableAll()
    {
        foreach (var go in gameObjects)
        {
            if (go != null)
                go.SetActive(true);
        }

        foreach (var behaviour in behaviours)
        {
            if (behaviour != null)
                behaviour.enabled = true;
        }
    }

    public void DisableAll()
    {
        foreach (var go in gameObjects)
        {
            if (go != null)
                go.SetActive(false);
        }

        foreach (var behaviour in behaviours)
        {
            if (behaviour != null)
                behaviour.enabled = false;
        }
    }
}
