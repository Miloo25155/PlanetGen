using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool shapeSettingsFoldout;
    [HideInInspector]
    public bool colorSettingsFoldout;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    MeshCollider[] meshColliders;

    TerrainFace[] terrainFaces;

    void Initialize()
    {
        shapeGenerator.UpdateSettings(shapeSettings);
        colorGenerator.UpdateSettings(colorSettings);

        if(meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        if (meshColliders == null || meshColliders.Length == 0)
        {
            meshColliders = new MeshCollider[6];
        }

        terrainFaces = new TerrainFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if(meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();

                meshColliders[i] = meshObj.AddComponent<MeshCollider>();
            }

            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;

            terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, meshColliders[i], resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }

    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        //GenerateMeshCollider();
        GenerateColor();
    }

    public void ClearPlanet()
    {
        meshFilters = new MeshFilter[6];
        meshColliders = new MeshCollider[6];

        for (int i = 0; i < 6; i++)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    //public void GenerateMeshCollider()
    //{
    //    planetCollider.sharedMesh = new Mesh();

    //    CombineInstance[] combine = new CombineInstance[meshFilters.Length];
    //    int index = 0;
    //    for (int i = 0; i < meshFilters.Length; i++)
    //    {
    //        if (meshFilters[i].sharedMesh == null) continue;
    //        combine[index].mesh = meshFilters[i].sharedMesh;
    //        combine[index++].transform = meshFilters[i].transform.localToWorldMatrix;
    //    }

    //    planetCollider.sharedMesh.CombineMeshes(combine);
    //}

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }
    }

    public void OnColorSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateColor();
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].ConstructMesh();
            }
        }

        colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColor()
    {
        colorGenerator.UpdateColors();
    }
}
