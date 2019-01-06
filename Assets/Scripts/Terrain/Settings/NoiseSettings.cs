using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum FilterType { Simple, Rigid };
    public FilterType filterType;
    
    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RigidNoiseSettings rigidNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        [Range(0, 5)]
        public float strength = 1;
        [Range(1, 10)]
        public int numberLayers = 1;

        [Range(0, 10)]
        public float baseRoughness = 1;
        [Range(0, 10)]
        public float roughness = 2;

        [Range(0, 2)]
        public float persistence = 0.5f;

        public Vector3 center;

        [Range(0, 10)]
        public float minValue;
    }

    [System.Serializable]
    public class RigidNoiseSettings: SimpleNoiseSettings
    {
        [Range(0, 10)]
        public float weightMultiplier = 0.8f;
    }

}
