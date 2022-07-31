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
    // List point
    [SerializeField, HideInInspector]
    GameObject[,] points;

   /// <summary>
    /// Init The Grid
    /// </summary>
    public void Init()
    {
        if(points != null)
        {
            for(int i = 0; i < 6; i++)
            {
                for (int y = 0; y < points.GetLength(1); y++)
                {
                    DestroyImmediate(points[i, y].gameObject);
                }
            }
        }
        points = new GameObject[6, resolution * resolution];
        InitGrid();
    }

    // init 6 faces of the planet
    void InitGrid()
    {
        // direction
        Vector3[] directions = {Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

        for (int i = 0; i < 6; i++)
        {
            // construct face of grid
            ConstructFace(i, directions[i]);
        }
    }

    // construct the face
    public void ConstructFace(int numFace, Vector3 dir)
    {
        // init direction
        Vector3 localUp = dir;
        // init the other 2 vec
        Vector3 axisX = new Vector3(localUp.y, localUp.z, localUp.x);
        Vector3 axisZ = Vector3.Cross(localUp, axisX);

        for (int i = 0; i < resolution * resolution; i++)
        {
            points[numFace, i] = new GameObject();
            points[numFace, i].transform.SetParent(gameObject.transform);
        }

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                // create point
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution -1);
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisX + (percent.y - 0.5f) * 2 * axisZ;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                points[numFace, i].transform.position = pointOnUnitSphere;
            }
        }
    }
}
