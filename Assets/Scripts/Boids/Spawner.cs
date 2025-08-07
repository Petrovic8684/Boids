using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public BoidSettings settings;
    public Boid prefab;
    public float spawnRadius = 10;
    public int spawnCount = 10;
    public BoxCollider boundsCollider;
    public Transform target;

    void Start()
    {
        var manager = FindObjectOfType<BoidManager>();

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = transform.position + Random.insideUnitSphere * spawnRadius;
            Boid boid = Instantiate(prefab, pos, Quaternion.LookRotation(Random.insideUnitSphere));
            BoidContext context = new BoidContext();

            List<IFlockingBehavior> behaviors = new()
            {
                new TargetSeekingBehavior(target),
                new AlignmentBehavior(),
                new CohesionBehavior(),
                new SeparationBehavior(),
                new CollisionAvoidanceBehavior(),
                new BoundsAvoidanceBehavior(boundsCollider)
            };

            boid.Initialize(settings, context, behaviors);
            manager.RegisterBoid(boid);
        }
    }
}
