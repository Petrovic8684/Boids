using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoidSettingsHandler))]
[RequireComponent(typeof(IBoidComputeHandler))]
public class BoidSpawner : MonoBehaviour, IBoidSpawner
{
    [SerializeField] private GameObject boidPrefab;
    [SerializeField] private Transform parent;
    [SerializeField] private float spawnRadius = 10;
    [SerializeField] private int spawnCount = 10;

    private IBoid prefab;
    private IBoidComputeHandler boidComputeHandler;
    private IBoidFactory boidFactory;
    private List<IFlockingBehavior> behaviors;
    private BoidSettings settings;

    private void OnValidate()
    {
        if (boidPrefab != null && boidPrefab.GetComponent<IBoid>() == null)
            Debug.LogError("Prefab must have a component that implements IBoid interface.", this);
    }


    private void Awake()
    {
        var settingsHandler = GetComponent<BoidSettingsHandler>();
        settings = settingsHandler.Settings;

        prefab = boidPrefab.GetComponent<IBoid>();
        boidComputeHandler = GetComponent<IBoidComputeHandler>();

        behaviors = new List<IFlockingBehavior>(GetComponents<IFlockingBehavior>());
        boidFactory = new BoidFactory(settings, prefab, parent, behaviors);
    }

    private void Start()
    {
        SpawnBoids();
    }

    public void SpawnBoids()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            var boid = boidFactory.CreateBoid(pos);
            boidComputeHandler.RegisterBoid(boid);
        }
    }
}
