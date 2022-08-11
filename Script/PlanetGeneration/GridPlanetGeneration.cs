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

    // vars priv
    Vector3 _pointNordOuestBack;
    Vector3 _pointNordOuestFront;
    Vector3 _pointNordEstBack;
    Vector3 _pointNordEstFront;
    Vector3 _pointSudOuestBack;
    Vector3 _pointSudOuestFront;
    Vector3 _pointSudEstBack;
    Vector3 _pointSudEstFront;

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
        // remove line
        foreach (var comp in gameObject.GetComponents<Component>())
        {
            if (comp is LineRenderer)
            {
                DestroyImmediate(comp);
            }
        }
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

        // get pos planet for convinience
        Vector3 pos = gameObject.transform.position;

        // create the base cube points
        // each point is named after the 3 face it's composed of
        _pointNordOuestBack = new Vector3(pos.x, pos.y, pos.z);
        _pointNordOuestFront = new Vector3(pos.x, pos.y, pos.z);
        _pointNordEstBack = new Vector3(pos.x, pos.y, pos.z);
        _pointNordEstFront = new Vector3(pos.x, pos.y, pos.z);
        _pointSudOuestBack = new Vector3(pos.x, pos.y, pos.z);
        _pointSudOuestFront = new Vector3(pos.x, pos.y, pos.z);
        _pointSudEstBack = new Vector3(pos.x, pos.y, pos.z);
        _pointSudEstFront = new Vector3(pos.x, pos.y, pos.z);

        // for test visualize the point
        if(planetSettings.PointTest)
        {
            LineRenderer l = gameObject.AddComponent<LineRenderer>();
            l.startColor = Color.black;
            l.endColor = Color.black;
            l.startWidth = 0.01f;
            l.endWidth = 0.01f;
            l.SetPosition(0, _pointNordOuestBack);
        }
    }

    
}
