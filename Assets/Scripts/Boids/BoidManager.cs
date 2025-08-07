using UnityEngine;
using System.Collections.Generic;

public class BoidManager : MonoBehaviour
{
    const int threadGroupSize = 1024;

    public BoidSettings settings;
    public ComputeShader compute;

    private List<Boid> boidsList = new();
    private Boid[] boids;

    public void RegisterBoid(Boid boid)
    {
        boidsList.Add(boid);
    }

    void Start()
    {
        boids = boidsList.ToArray();
    }

    void Update()
    {
        if (boids == null || boids.Length == 0) return;

        int numBoids = boids.Length;
        var boidData = new BoidData[numBoids];

        for (int i = 0; i < numBoids; i++)
        {
            boidData[i].position = boids[i].transform.position;
            boidData[i].direction = boids[i].transform.forward;
        }

        var boidBuffer = new ComputeBuffer(numBoids, BoidData.Size);
        boidBuffer.SetData(boidData);

        compute.SetBuffer(0, "boids", boidBuffer);
        compute.SetInt("numBoids", numBoids);
        compute.SetFloat("viewRadius", settings.perceptionRadius);
        compute.SetFloat("avoidRadius", settings.avoidanceRadius);

        int threadGroups = Mathf.CeilToInt(numBoids / (float)threadGroupSize);
        compute.Dispatch(0, threadGroups, 1, 1);

        boidBuffer.GetData(boidData);

        for (int i = 0; i < numBoids; i++)
        {
            boids[i].UpdatePerception(
                boidData[i].flockHeading,
                boidData[i].flockCentre,
                boidData[i].avoidanceHeading,
                boidData[i].numFlockmates
            );

            boids[i].Tick();
        }

        boidBuffer.Release();
    }
}
