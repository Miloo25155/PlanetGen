using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSphere : MonoBehaviour
{
    //[Range(2, 256)]
    //public int resolution = 10;
    //public bool autoUpdate = true;
    //public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    //public FaceRenderMask faceRenderMask;

    //[SerializeField, HideInInspector]
    //MeshFilter[] meshFilters;
    //MeshCollider[] meshColliders;

    //WaterFace[] waterFaces;

    public WaterSettings waterSettings;
    //public WaveSettings waveSettings;
    [HideInInspector]
    public bool waterSettingsFoldout;

    public Material material;

    MeshFilter meshFilter;

    //public Material material;

    //void Initialize()
    //{
    //    if (meshFilters == null || meshFilters.Length == 0)
    //    {
    //        meshFilters = new MeshFilter[6];
    //    }
    //    if (meshColliders == null || meshColliders.Length == 0)
    //    {
    //        meshColliders = new MeshCollider[6];
    //    }

    //    waterFaces = new WaterFace[6];

    //    Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

    //    for (int i = 0; i < 6; i++)
    //    {
    //        if (meshFilters[i] == null)
    //        {
    //            GameObject meshObj = new GameObject("mesh");
    //            meshObj.transform.parent = transform;

    //            meshObj.AddComponent<MeshRenderer>();
    //            meshFilters[i] = meshObj.AddComponent<MeshFilter>();
    //            meshFilters[i].sharedMesh = new Mesh();
    //        }

    //        meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = material;
    //        //meshFilters[i].GetComponent<MeshRenderer>().material = colorSettings.planetMaterial;

    //        waterFaces[i] = new WaterFace(meshFilters[i].sharedMesh, resolution, directions[i]);
    //        bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
    //        meshFilters[i].gameObject.SetActive(renderFace);
    //    }
    //}

    void Initialize()
    {
        var currentFilter = GetComponent<MeshFilter>();
        if (currentFilter == null)
        {
            meshFilter = gameObject.AddComponent<MeshFilter>();
            gameObject.AddComponent<MeshRenderer>();
        }
        else
        {
            meshFilter = currentFilter;
        }
    }

    public void GenerateWaterSphere()
    {
        Initialize();
        GenerateMesh();
    }

    //public void ClearWaterSphere()
    //{
    //    meshFilters = null;
    //    meshColliders = null;
    //    waterFaces = null;

    //    int childCount = transform.childCount;
    //    for (int i = childCount - 1; i >= 0; i--)
    //    {
    //        DestroyImmediate(transform.GetChild(i).gameObject);
    //    }
    //}

    public void ClearWaterSphere()
    {
        meshFilter.sharedMesh = new Mesh();
    }

    void GenerateMesh()
    {
        //for (int i = 0; i < 6; i++)
        //{
        //    if (meshFilters[i].gameObject.activeSelf)
        //    {
        //        waterFaces[i].ConstructMesh(waterSettings.sphereRadius, waterSettings.lowPolyGeneration);
        //        meshColliders[i] = waterFaces[i].InitMeshCollider(meshFilters[i].gameObject);
        //    }
        //}
        if(meshFilter != null)
        {
            Mesh newMesh = HexaSphere.BuildMesh(waterSettings.recursionLevel, waterSettings.sphereRadius);


            //if (waterSettings.lowPolyGeneration)
            //{
            //    Vector3[] flatVertices = new Vector3[newMesh.triangles.Length];
            //    //Vector2[] flatUvs = new Vector2[newMesh.triangles.Length];

            //    for (int i = 0; i < newMesh.triangles.Length; i++)
            //    {
            //        flatVertices[i] = newMesh.vertices[newMesh.triangles[i]];
            //        //flatUvs[i] = newMesh.uv[newMesh.triangles[i]];
            //        newMesh.triangles[i] = i;
            //    }

            //    newMesh.vertices = flatVertices;
            //    //newMesh.uv = flatUvs;
            //}

            meshFilter.sharedMesh = newMesh;
            meshFilter.sharedMesh.RecalculateNormals();

            meshFilter.GetComponent<MeshRenderer>().sharedMaterial = material;
        }
    }
}
