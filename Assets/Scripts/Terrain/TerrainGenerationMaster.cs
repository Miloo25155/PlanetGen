using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerationMaster : MonoBehaviour
{
    public GameObject planetPrefab;
    public GameObject waterPrefab;
    //public Texture2D planetGeneratedTexture;

    WaterSphere waterSphere;

    void Start()
    {
        planetPrefab.GetComponent<Planet>().GeneratePlanet();
        waterSphere = waterPrefab.GetComponent<WaterSphere>();
        waterSphere.GenerateWaterSphere();
    }
    void Update()
    {

    }

}
