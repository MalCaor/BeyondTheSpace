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

        // create the planet faces if enable
        if(planetSettings.faceUp){
            // face Nord
            //       ^ Back
            //     +---+
            //Est< |   | > Ouest
            //     +---+
            //      \/ Front
            InitFaceGrid(0, _pointNordEstBack, _pointNordOuestBack, _pointNordEstFront, _pointNordOuestFront, FaceNord);
        }
        if(planetSettings.faceRight){
            // face Est
            //        ^ Nord
            //      +---+
            //Back< |   | > Front
            //      +---+
            //       \/ Sud
            InitFaceGrid(1, _pointNordEstBack, _pointNordEstFront, _pointSudEstBack, _pointSudEstFront, FaceEst);
        }
        if(planetSettings.faceLeft){
            // face Ouest
            //         ^ Nord
            //       +---+
            //Front< |   | > Back
            //       +---+
            //        \/ Sud
            InitFaceGrid(2, _pointNordOuestFront, _pointNordOuestBack, _pointSudOuestFront, _pointSudOuestBack, FaceOuest);
        }
        if(planetSettings.faceForward){
            // face Front
            //         ^ Nord
            //       +---+
            //Ouest< |   | > Est
            //       +---+
            //        \/ Sud
            InitFaceGrid(3, _pointNordOuestFront, _pointNordEstFront, _pointSudOuestFront, _pointSudEstFront, FaceFront);
        }
        if(planetSettings.faceBack){
            // face Back
            //       ^ Nord
            //     +---+
            //Est< |   | > Ouest
            //     +---+
            //      \/ Sud
            InitFaceGrid(4, _pointNordEstBack, _pointNordOuestBack, _pointSudEstBack, _pointSudOuestBack, FaceBack);
        }
        if(planetSettings.faceDown){
            // face Sud
            //       ^ Back
            //     +---+
            //Est< |   | > Ouest
            //     +---+
            //      \/ Front
            InitFaceGrid(5, _pointSudEstBack, _pointSudOuestBack, _pointSudEstFront, _pointSudOuestFront, FaceSud);
        }
    }

    void InitFaceGrid(int numFace, Vector3 NOGlobal, Vector3 NEGlobal, Vector3 SOGlobal, Vector3 SEGlobal, List<GridTile> face)
    {
        int numTiles = planetSettings.resolution * planetSettings.resolution;
        int N = 0;
        int O = 0;
        int S = (planetSettings.resolution-1); // num start at 0 so -1
        int E = (planetSettings.resolution-1); // num start at 0 so -1

        Vector3 NO;
        Vector3 NE;
        Vector3 SO;
        Vector3 SE;

        for (int nTile = 0; nTile < numTiles; nTile++)
        {
            // get coor
            NO = Vector3.Lerp(NOGlobal, NEGlobal, (float)O/(float)planetSettings.resolution);
            NE = Vector3.Lerp(NOGlobal, NEGlobal, (float)(O+1)/(float)planetSettings.resolution);
            SO = Vector3.Lerp(NOGlobal, SOGlobal, (float)(N+1)/(float)planetSettings.resolution);
            SE = Vector3.Reflect(NO, Vector3.Cross(SO, NE).normalized);

            // set tile
            GridTile t = new GridTile(numFace, N, O, E, S);
            t.InitSquare(NO, NE, SO, SE);
            t.tile.transform.parent = gameObject.transform;
            
            // go to the Est
            E--;
            O++;
            // check if change line
            if(E<0)
            {
                O = 0;
                E = (planetSettings.resolution-1);
                N++;
                S--;
            }
        }
    }
}
