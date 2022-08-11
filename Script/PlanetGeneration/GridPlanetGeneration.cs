using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlanetGeneration : MonoBehaviour
{
    // vars Pub
    // setting planet
    public PlanetGenerationSettings planetSettings;
    // the 6 faces of a planet
    public List<GridTile> FaceNord;
    public List<GridTile> FaceEst;
    public List<GridTile> FaceOuest;
    public List<GridTile> FaceFront;
    public List<GridTile> FaceBack;
    public List<GridTile> FaceSud;

    /// <summary>
    /// Init The Grid
    /// </summary>
    public void Init()
    {
        DestroyChild();
        InitGrid();
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

    /// <summary>
    /// init 6 faces of the planet
    /// </summary>
    void InitGrid()
    {
        // set up the list Faces
        FaceNord = new List<GridTile>();
        FaceEst = new List<GridTile>();
        FaceOuest = new List<GridTile>();
        FaceFront = new List<GridTile>();
        FaceBack = new List<GridTile>();
        FaceSud = new List<GridTile>();

        Vector3 pos = gameObject.transform.position;

        // create the base cube points
        // each point is named after the 3 face it's composed of
        Vector3 PointNordOuestBack = new Vector3(pos.x, pos.y, pos.z);
        Vector3 PointNordOuestFront = new Vector3(pos.x, pos.y, pos.z);
        Vector3 PointNordEstBack = new Vector3(pos.x, pos.y, pos.z);
        Vector3 PointNordEstFront = new Vector3(pos.x, pos.y, pos.z);
        Vector3 PointSudOuestBack = new Vector3(pos.x, pos.y, pos.z);
        Vector3 PointSudOuestFront = new Vector3(pos.x, pos.y, pos.z);
        Vector3 PointSudEstBack = new Vector3(pos.x, pos.y, pos.z);
        Vector3 PointSudEstFront = new Vector3(pos.x, pos.y, pos.z);
    }

    
}
