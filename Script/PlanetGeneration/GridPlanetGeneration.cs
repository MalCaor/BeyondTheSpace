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
        points = new GameObject[6, (planetSettings.resolution), (planetSettings.resolution)];
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
        ConstructFace(0, Vector3.down);
        ConstructFace(1, Vector3.back);
        ConstructFace(2, Vector3.up);
        ConstructFace(3, Vector3.forward);
        ConstructFace(4, Vector3.left);
        ConstructFace(5, Vector3.right);
    }

    // construct the face
    public void ConstructFace(int numFace, Vector3 dir)
    {
        // init direction
        Vector3 localUp = dir;
        // init the other 2 vec
        Vector3 axisX = new Vector3(localUp.y, localUp.z, localUp.x);
        Vector3 axisZ = Vector3.Cross(localUp, axisX);
        int minx = 0;
        int minz = 0;
        int maxX = planetSettings.resolution -1;
        int maxZ = planetSettings.resolution -1;
        // evite des cube de se superposer
        switch (numFace)
        {
            case 0:
                // down
                minx = 0;
                minz = 1;
                maxX = planetSettings.resolution;
                maxZ = planetSettings.resolution;
                break;
            case 1:
                // back
                minx = 0;
                minz = 0;
                maxX = planetSettings.resolution-1;
                maxZ = planetSettings.resolution;
                break;
            case 2:
                // up
                minx = 0;
                minz = 0;
                maxX = planetSettings.resolution;
                maxZ = planetSettings.resolution-1;
                break;
            case 3:
                // forward
                minx = 0;
                minz = 0;
                maxX = planetSettings.resolution-1;
                maxZ = planetSettings.resolution;
                break;
            case 4:
                // left
                minx = 1;
                minz = 1;
                maxX = planetSettings.resolution-1;
                maxZ = planetSettings.resolution-1;
                break;
            case 5:
                // right
                minx = 1;
                minz = 1;
                maxX = planetSettings.resolution-1;
                maxZ = planetSettings.resolution-1;
                break;
            
            default:
                break;
        }

        for (int x = minx; x < maxX; x++)
        {
            for (int z = minz; z < maxZ; z++)
            {
                // test prefab Prefab/Test/CubePointTest
                points[numFace, x, z] = Instantiate(Resources.Load<GameObject>("Prefab/Test/CubePointTest")) as GameObject;
                points[numFace, x, z].transform.SetParent(gameObject.transform);
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
