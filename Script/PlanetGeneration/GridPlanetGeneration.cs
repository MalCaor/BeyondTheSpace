using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlanetGeneration : MonoBehaviour
{
    // vars Pub
    
    public PlanetGenerationSettings planetSettings;

    // vars Priv
    // List point
    [SerializeField]
    // points[CubeFace, x, z]
    GameObject[,,,] points;
    // num of corou ConstructFace
    int numCorouConstructFace = 0;

   /// <summary>
    /// Init The Grid
    /// </summary>
    public void Init()
    {
        DestroyChild();
        points = new GameObject[6, (planetSettings.resolution), (planetSettings.resolution), planetSettings.height];
        StartCoroutine(InitGrid());
        // insert wait for Init To finish before continues
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
    IEnumerator InitGrid()
    {
        numCorouConstructFace = 0;
        if(planetSettings.faceDown){
            StartCoroutine(ConstructFace(0, Vector3.down));
            numCorouConstructFace += 1;
        }
        if(planetSettings.faceBack){
            StartCoroutine(ConstructFace(1, Vector3.back));
            numCorouConstructFace += 1;
        }
        if(planetSettings.faceUp){
            StartCoroutine(ConstructFace(2, Vector3.up));
            numCorouConstructFace += 1;
        }
        if(planetSettings.faceForward){
            StartCoroutine(ConstructFace(3, Vector3.forward));
            numCorouConstructFace += 1;
        }
        if(planetSettings.faceLeft){
            StartCoroutine(ConstructFace(4, Vector3.left));
            numCorouConstructFace += 1;
        }
        if(planetSettings.faceRight){
            StartCoroutine(ConstructFace(5, Vector3.right));
            numCorouConstructFace += 1;
        }
        while (numCorouConstructFace != 0){
            yield return null;
        }
        Debug.Log("All Grid Initialized");
    }

    // construct the face
    IEnumerator ConstructFace(int numFace, Vector3 dir)
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
                for (int h = 0; h < planetSettings.height; h++)
                {
                    // test prefab Prefab/Test/CubePointTest
                    if(planetSettings.PointTest)
                    {
                        // load test prefab
                        points[numFace, x, z, h] = Instantiate(Resources.Load<GameObject>("Prefab/Test/CubePointTest")) as GameObject;
                    }else{
                        // load real point
                        points[numFace, x, z, h] = Instantiate(Resources.Load<GameObject>("Prefab/Grid/GridPoint")) as GameObject;
                    }
                    
                    points[numFace, x, z, h].transform.SetParent(gameObject.transform);
                    Vector2 percent = new Vector2(x, z) / (planetSettings.resolution -1);
                    Vector2 percentHeight = new Vector2(h, h) / (planetSettings.height -1);
                    // get point on cube
                    Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisX + (percent.y - 0.5f) * 2 * axisZ;
                    // get on shphere
                    Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                    // multiply with radius
                    Vector3 finalpoint = pointOnUnitSphere * (planetSettings.radius + ((float)h / 1.5f));
                    // transform the point to it's final location
                    points[numFace, x, z, h].transform.position = finalpoint;
                    //name it
                    points[numFace, x, z, h].name = "Point Face " + (numFace +1) + " : " + x + ", " + z + ", " + h;
                    yield return null;
                }
            }
        }
        Debug.Log("Grid " + numFace +" Initiated");
        numCorouConstructFace -= 1;
    }
}
