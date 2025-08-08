using UnityEngine;
using System.Collections.Generic;

public class BoidManager : MonoBehaviour
{
    const int threadGroupSize = 1024;

    public BoidSettings settings;
    public ComputeShader compute;

    private List<Boid> boidsList = new();
    internal Boid[] Boids { get; set; }

    public void RegisterBoid(Boid boid)
    {
        boidsList.Add(boid);
    }

    void Start()
    {
        Boids = boidsList.ToArray();
    }

    void Update()
    {
        if (Boids == null || Boids.Length == 0) return;

        int numBoids = Boids.Length;
        var boidData = new BoidData[numBoids];

        for (int i = 0; i < numBoids; i++)
        {
            boidData[i].position = Boids[i].transform.position;
            boidData[i].direction = Boids[i].transform.forward;
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
            Boids[i].UpdatePerception(
                boidData[i].flockHeading,
                boidData[i].flockCentre,
                boidData[i].avoidanceHeading,
                boidData[i].numFlockmates
            );

            Boids[i].Tick();
        }

        boidBuffer.Release();
    }
}
