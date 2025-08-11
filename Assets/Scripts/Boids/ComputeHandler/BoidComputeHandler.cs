using System;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(BoidSettingsHandler))]
public class BoidComputeHandler : MonoBehaviour, IBoidComputeHandler
{
    [SerializeField] private ComputeShader computeShader;

    const int threadGroupSize = 1024;
    private List<IBoid> boids = new();
    private IBoid[] boidsArray;
    private BoidSettings settings;

    public IEnumerable<IBoid> AllBoids => boidsArray ?? Array.Empty<IBoid>();

    private void Awake()
    {
        var settingsHandler = GetComponent<BoidSettingsHandler>();
        settings = settingsHandler.Settings;
    }


    public void RegisterBoid(IBoid boid)
    {
        boids.Add(boid);
        boidsArray = boids.ToArray();
    }

    private void Update()
    {
        if (boidsArray == null || boidsArray.Length == 0) return;

        var boidData = new BoidData[boidsArray.Length];
        for (int i = 0; i < boidsArray.Length; i++)
        {
            var ctx = boidsArray[i].Context;
            boidData[i].position = ctx.Position;
            boidData[i].direction = ctx.Forward;
        }

        var boidBuffer = new ComputeBuffer(boidsArray.Length, BoidData.Size);
        boidBuffer.SetData(boidData);

        computeShader.SetBuffer(0, "boids", boidBuffer);
        computeShader.SetInt("numBoids", boidsArray.Length);
        computeShader.SetFloat("viewRadius", settings.perceptionRadius);
        computeShader.SetFloat("avoidRadius", settings.avoidanceRadius);

        int threadGroups = Mathf.CeilToInt(boidsArray.Length / (float)threadGroupSize);
        computeShader.Dispatch(0, threadGroups, 1, 1);
        boidBuffer.GetData(boidData);

        for (int i = 0; i < boidsArray.Length; i++)
        {
            boidsArray[i].UpdatePerception(
                boidData[i].flockHeading,
                boidData[i].flockCentre,
                boidData[i].avoidanceHeading,
                boidData[i].numFlockmates
            );
            boidsArray[i].Simulate(Time.deltaTime);
        }

        boidBuffer.Release();
    }
}
