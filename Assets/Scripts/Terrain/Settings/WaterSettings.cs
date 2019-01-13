using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WaterSettings : ScriptableObject
{
    public float sphereRadius = 1;
    [Range(0, 5)]
    public int recursionLevel = 0;
    public bool lowPolyGeneration;

    [Range(0,1)]
    public float glow = 1;
    [Range(0, 1)]
    public float metallic = 1;

    public float speedX = 1;
    public float speedY = 1;

    public Color mainColor;
    public Color secondaryColor;

    public float fresnelPower;

    public float waterReflect;
}
