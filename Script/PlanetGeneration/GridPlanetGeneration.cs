using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlanetGeneration : MonoBehaviour
{
    // vars Pub
    // resolution
    [Range(2, 256)]
    public int resolution = 10;

    // vars Priv
    // List MeshFilters
    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    // List Faces
    GridFace[] grifFaces;

   /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    void OnValidate()
    {
        InitGrid();
        GenerateMesh();
    }

    // init 6 faces of the planet
    void InitGrid()
    {
        if(meshFilters == null || meshFilters.Length == 0)
        {
            // the 6 faces of a cube
            meshFilters = new MeshFilter[6];
        }
        grifFaces = new GridFace[6];

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < 6; i++)
        {
            GameObject meshObj = new GameObject("mesh");
            meshObj.transform.parent = transform;

            meshObj.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
            meshFilters[i] = meshObj.AddComponent<MeshFilter>();
            meshFilters[i].sharedMesh = new Mesh();

            grifFaces[i] = new GridFace(meshFilters[i].sharedMesh, resolution, directions[i]);
        }
    }

    void GenerateMesh()
    {
        foreach (GridFace grid in grifFaces)
        {
            grid.ConstructFace();
        }
    }
}
