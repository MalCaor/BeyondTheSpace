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

        // get pos and rad planet for convinience
        Vector3 pos = gameObject.transform.position;
        float rad =  planetSettings.radius;

        // create the base cube points
        // each point is named after the 3 face it's composed of
        _pointNordOuestBack = new Vector3(pos.x+rad, pos.y+rad, pos.z-rad);
        _pointNordOuestFront = new Vector3(pos.x+rad, pos.y+rad, pos.z+rad);
        _pointNordEstBack = new Vector3(pos.x-rad, pos.y+rad, pos.z-rad);
        _pointNordEstFront = new Vector3(pos.x-rad, pos.y+rad, pos.z+rad);
        _pointSudOuestBack = new Vector3(pos.x+rad, pos.y-rad, pos.z-rad);
        _pointSudOuestFront = new Vector3(pos.x+rad, pos.y-rad, pos.z+rad);
        _pointSudEstBack = new Vector3(pos.x-rad, pos.y-rad, pos.z-rad);
        _pointSudEstFront = new Vector3(pos.x-rad, pos.y-rad, pos.z+rad);

        // for test visualize the point
        if(planetSettings.PointTest)
        {
            GameObject gLineUp = new GameObject("Line Up");
            gLineUp.transform.parent = gameObject.transform;
            LineRenderer lUp = gLineUp.AddComponent<LineRenderer>();
            lUp.startColor = Color.black;
            lUp.endColor = Color.black;
            lUp.startWidth = 0.01f;
            lUp.endWidth = 0.01f;
            lUp.positionCount = 5;
            lUp.SetPosition(0, _pointNordOuestBack);
            lUp.SetPosition(1, _pointNordOuestFront);
            lUp.SetPosition(2, _pointNordEstFront);
            lUp.SetPosition(3, _pointNordEstBack);
            lUp.SetPosition(4, _pointNordOuestBack);

            GameObject gLineDown = new GameObject("Line Down");
            gLineDown.transform.parent = gameObject.transform;
            LineRenderer lDown = gLineDown.AddComponent<LineRenderer>();
            lDown.startColor = Color.black;
            lDown.endColor = Color.black;
            lDown.startWidth = 0.01f;
            lDown.endWidth = 0.01f;
            lDown.positionCount = 5;
            lDown.SetPosition(0, _pointSudOuestBack);
            lDown.SetPosition(1, _pointSudOuestFront);
            lDown.SetPosition(2, _pointSudEstFront);
            lDown.SetPosition(3, _pointSudEstBack);
            lDown.SetPosition(4, _pointSudOuestBack);

            GameObject gLineOB = new GameObject("Line Ouest Back");
            gLineOB.transform.parent = gameObject.transform;
            LineRenderer lOB = gLineOB.AddComponent<LineRenderer>();
            lOB.startColor = Color.black;
            lOB.endColor = Color.black;
            lOB.startWidth = 0.01f;
            lOB.endWidth = 0.01f;
            lOB.positionCount = 2;
            lOB.SetPosition(0, _pointNordOuestBack);
            lOB.SetPosition(1, _pointSudOuestBack);

            GameObject gLineOF = new GameObject("Line Ouest Front");
            gLineOF.transform.parent = gameObject.transform;
            LineRenderer lOF = gLineOF.AddComponent<LineRenderer>();
            lOF.startColor = Color.black;
            lOF.endColor = Color.black;
            lOF.startWidth = 0.01f;
            lOF.endWidth = 0.01f;
            lOF.positionCount = 2;
            lOF.SetPosition(0, _pointNordOuestFront);
            lOF.SetPosition(1, _pointSudOuestFront);

            GameObject gLineEB = new GameObject("Line Est Back");
            gLineEB.transform.parent = gameObject.transform;
            LineRenderer lEB = gLineEB.AddComponent<LineRenderer>();
            lEB.startColor = Color.black;
            lEB.endColor = Color.black;
            lEB.startWidth = 0.01f;
            lEB.endWidth = 0.01f;
            lEB.positionCount = 2;
            lEB.SetPosition(0, _pointNordEstBack);
            lEB.SetPosition(1, _pointSudEstBack);

            GameObject gLineEF = new GameObject("Line Est Front");
            gLineEF.transform.parent = gameObject.transform;
            LineRenderer lEF = gLineEF.AddComponent<LineRenderer>();
            lEF.startColor = Color.black;
            lEF.endColor = Color.black;
            lEF.startWidth = 0.01f;
            lEF.endWidth = 0.01f;
            lEF.positionCount = 2;
            lEF.SetPosition(0, _pointNordEstFront);
            lEF.SetPosition(1, _pointSudEstFront);
        }
    }

    
}
