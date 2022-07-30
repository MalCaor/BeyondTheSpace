using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlanetGeneration : MonoBehaviour
{
    // vars Pub
    // resolution
    [Range(2, 100)]
    public int resolution = 10;

    // vars Priv
    // List Faces
    GridFace[] grifFaces;

   /// <summary>
    /// Called when the script is loaded or a value is changed in the
    /// inspector (Called in the editor only).
    /// </summary>
    public void Init()
    {
        InitGrid();
        ConstructFace();
    }

    // init 6 faces of the planet
    void InitGrid()
    {
        grifFaces = new GridFace[6];

        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < 6; i++)
        {
            grifFaces[i] = gameObject.AddComponent(typeof(GridFace)) as GridFace;
            grifFaces[i].ConstructGrid(transform, resolution, directions[i]);
        }
    }

    void ConstructFace()
    {
        foreach (GridFace grid in grifFaces)
        {
            grid.ConstructFace();
        }
    }
}
