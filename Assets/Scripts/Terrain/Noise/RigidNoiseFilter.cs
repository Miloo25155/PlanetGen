using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter: INoiseFilter
{
    Noise noise = new Noise();
    NoiseSettings.RigidNoiseSettings settings;

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRoughness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numberLayers; i++)
        {
            float v = noise.Evaluate(point * frequency + settings.center);
            v = 1 - Mathf.Abs(v);
            v *= v;
            v *= weight;

            weight = Mathf.Clamp01(v * settings.weightMultiplier);

            noiseValue += v * amplitude;

            frequency *= settings.roughness;
            amplitude *= settings.persistence;
        }

        noiseValue = Mathf.Max(settings.minValue, noiseValue);
        return noiseValue * settings.strength;
    }
}
