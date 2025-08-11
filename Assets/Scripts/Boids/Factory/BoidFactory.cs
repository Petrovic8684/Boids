using UnityEngine;
using System.Collections.Generic;

public class BoidFactory : IBoidFactory
{
    private readonly BoidSettings settings;
    private readonly IBoid prefab;
    private readonly Transform parent;
    private readonly List<IFlockingBehavior> behaviors;

    public BoidFactory(BoidSettings settings, IBoid prefab, Transform parent, List<IFlockingBehavior> behaviors)
    {
        this.settings = settings;
        this.prefab = prefab;
        this.parent = parent;
        this.behaviors = behaviors;
    }

    public IBoid CreateBoid(Vector3 position, Transform parentOverride = null)
    {
        var parentTransform = parentOverride ?? parent;
        var boid = Object.Instantiate(prefab as Boid, position, Quaternion.LookRotation(Random.insideUnitSphere), parentTransform);

        var context = new BoidContext
        {
            RuntimeAlignWeight = settings.alignWeight,
            RuntimeCohesionWeight = settings.cohesionWeight,
            RuntimeSeperateWeight = settings.seperateWeight
        };

        var movement = new BoidMovement(settings, Random.insideUnitSphere.normalized);
        boid.Initialize(settings, context, behaviors, movement);

        return boid;
    }
}
