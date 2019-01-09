using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerationMaster : MonoBehaviour
{
    public GameObject planetPrefab;
    //public Texture2D planetGeneratedTexture;

    void Start()
    {
        planetPrefab.GetComponent<Planet>().GeneratePlanet();
    }
}
