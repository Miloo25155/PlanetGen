using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSphere : MonoBehaviour
{
    [Range(2, 256)]
    public int resolution = 10;
    public bool autoUpdate = true;
    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back };
    public FaceRenderMask faceRenderMask;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    MeshCollider[] meshColliders;

    WaterFace[] waterFaces;

    public WaterSettings waterSettings;
    [HideInInspector]
    public bool waterSettingsFoldout;

    public Material material;

    void Initialize()
    {
        if (meshFilters == null || meshFilters.Length == 0)
        {
            meshFilters = new MeshFilter[6];
        }
        if (meshColliders == null || meshColliders.Length == 0)
        {
            meshColliders = new MeshCollider[6];
        }

        waterFaces = new WaterFace[6];

        Vector3[] directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i] == null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.parent = transform;

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
            }

            meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = material;
            //meshFilters[i].GetComponent<MeshRenderer>().material = colorSettings.planetMaterial;

            waterFaces[i] = new WaterFace(meshFilters[i].sharedMesh, resolution, directions[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace);
        }
    }

    public void GenerateWaterSphere()
    {
        Initialize();
        GenerateMesh();
    }

    public void ClearWaterSphere()
    {
        meshFilters = null;
        meshColliders = null;
        waterFaces = null;

        int childCount = transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    void GenerateMesh()
    {
        for (int i = 0; i < 6; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                waterFaces[i].ConstructMesh(waterSettings.sphereRadius);
                meshColliders[i] = waterFaces[i].InitMeshCollider(meshFilters[i].gameObject);
            }
        }
    }
}
