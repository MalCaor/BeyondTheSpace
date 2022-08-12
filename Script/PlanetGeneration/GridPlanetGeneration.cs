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
        // low Point
        Vector3 NO;
        Vector3 NE;
        Vector3 SO;
        Vector3 SE;

        // hight Point
        Vector3 NOH;
        Vector3 NEH;
        Vector3 SOH;
        Vector3 SEH;

        // inter point for calculation
        Vector3 IntermedNord0;
        Vector3 IntermedNord1;
        Vector3 IntermedSud0;
        Vector3 IntermedSud1;

        float heightTileMulti = 1;
        for (int h = 0; h < planetSettings.height; h++)
        {
            for (int O = 0; O < planetSettings.resolution; O++)
            {
                for (int N = 0; N < planetSettings.resolution; N++)
                {
                    // set intermed
                    IntermedNord0 = Vector3.Lerp(NOGlobal, NEGlobal, (float)O/(float)planetSettings.resolution); 
                    IntermedNord1 = Vector3.Lerp(NOGlobal, NEGlobal, (float)(O+1)/(float)planetSettings.resolution); 
                    IntermedSud0 = Vector3.Lerp(SOGlobal, SEGlobal, (float)O/(float)planetSettings.resolution); 
                    IntermedSud1 = Vector3.Lerp(SOGlobal, SEGlobal, (float)(O+1)/(float)planetSettings.resolution); 
                    // set point
                    NO = Vector3.Lerp(IntermedNord0, IntermedSud0, (float)N/(float)planetSettings.resolution) * (h+1);
                    NE = Vector3.Lerp(IntermedNord1, IntermedSud1, (float)N/(float)planetSettings.resolution) * (h+1);
                    SO = Vector3.Lerp(IntermedNord0, IntermedSud0, (float)(N+1)/(float)planetSettings.resolution) * (h+1);
                    SE = Vector3.Lerp(IntermedNord1, IntermedSud1, (float)(N+1)/(float)planetSettings.resolution) * (h+1);

                    NOH = NO * (h+2);
                    NEH = NE * (h+2);
                    SOH = SO * (h+2);
                    SEH = SE * (h+2);

                    if(planetSettings.Sphere)
                    {
                        NO = NO.normalized * planetSettings.radius * (float)(heightTileMulti);
                        NE = NE.normalized * planetSettings.radius * (float)(heightTileMulti);
                        SO = SO.normalized * planetSettings.radius * (float)(heightTileMulti);
                        SE = SE.normalized * planetSettings.radius * (float)(heightTileMulti);

                        NOH = NOH.normalized * planetSettings.radius * (float)(heightTileMulti + planetSettings.tileHeight);
                        NEH = NEH.normalized * planetSettings.radius * (float)(heightTileMulti + planetSettings.tileHeight);
                        SOH = SOH.normalized * planetSettings.radius * (float)(heightTileMulti + planetSettings.tileHeight);
                        SEH = SEH.normalized * planetSettings.radius * (float)(heightTileMulti + planetSettings.tileHeight);
                    }

                    // set tile
                    GridTile t = new GridTile(numFace, N, O, planetSettings.resolution-O, planetSettings.resolution-N);
                    t.InitSquare(NO, NE, SO, SE, NOH, NEH, SOH, SEH);
                    t.tile.transform.parent = gameObject.transform;
                }
            }
            heightTileMulti = heightTileMulti + planetSettings.tileHeight;
        }

        
        
            
    }
}
