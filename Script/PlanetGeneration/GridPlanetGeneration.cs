using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlanetGeneration : MonoBehaviour
{
    // vars Pub
    
    public PlanetGenerationSettings planetSettings;

    // vars Priv
    // List point
    [SerializeField, HideInInspector]
    // points[CubeFace, x, z]
    GameObject[,,] points;

   /// <summary>
    /// Init The Grid
    /// </summary>
    public void Init()
    {
        DestroyChild();
        points = new GameObject[6, (planetSettings.resolution -1), (planetSettings.resolution -1)];
        InitGrid();
    }

    /// <summary>
    /// Destroy all point
    /// </summary>
    void DestroyChild()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--) 
        {
            GameObject.DestroyImmediate(transform.GetChild( i ).gameObject);
        }
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

        for (int x = 0; x < (planetSettings.resolution -1); x++)
        {
            for (int z = 0; z < (planetSettings.resolution -1); z++)
            {
                // test prefab Prefab/Test/CubePointTest
                points[numFace, x, z] = Instantiate(Resources.Load<GameObject>("Prefab/Test/CubePointTest")) as GameObject;
                points[numFace, x, z].transform.SetParent(gameObject.transform);
            }
        }

        for (int x = 0; x < planetSettings.resolution -1; x++)
        {
            for (int z = 0; z < planetSettings.resolution -1; z++)
            {
                Vector2 percent = new Vector2(x, z) / (planetSettings.resolution -1);
                // get point on cube
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisX + (percent.y - 0.5f) * 2 * axisZ;
                // get on shphere
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                // multiply with radius
                Vector3 finalpoint = pointOnUnitSphere * planetSettings.radius;
                // transform the point to it's final location
                points[numFace, x, z].transform.position = finalpoint;
                //name it
                points[numFace, x, z].name = "Point Face " + (numFace +1) + " : " + x + ", " + z;
            }
        }
    }
}
