using UnityEngine;
using System.Collections.Generic;

public class IslandSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> islandPrefabs;
    [SerializeField] private Transform parent;
    [SerializeField] private int islandCount = 10;
    [SerializeField] private float spawnRadius = 50f;

    private void Start()
    {
        SpawnIslands();
    }

    private void SpawnIslands()
    {
        if (islandPrefabs == null || islandPrefabs.Count == 0)
            return;

        for (int i = 0; i < islandCount; i++)
        {
            Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;

            GameObject prefab = islandPrefabs[Random.Range(0, islandPrefabs.Count)];
            Instantiate(prefab, randomPos, prefab.transform.rotation, parent);
        }
    }
}
