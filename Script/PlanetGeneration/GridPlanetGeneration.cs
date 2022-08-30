using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlanetGeneration : MonoBehaviour
{
    // var planet target
    public GameObject planet;
    // planet data
    public GridPlanetData planetData;

    // vars Pub
    // setting planet
    public PlanetGenerationSettings planetSettings;
    

    /// <summary>
    /// Init The Grid
    /// </summary>
    public void Init()
    {
        DestroyChild();
        InitGrid();
        InitProxyTile();
        InitTileGridGameObject();
        SetTilesManager();
    }

    /// <summary>
    /// Destroy all point
    /// </summary>
    void DestroyChild()
    {
        // destroy data
        GameObject.DestroyImmediate(planet.GetComponent<GridPlanetData>());
        int childs = planet.transform.childCount;
        for (int i = childs - 1; i >= 0; i--) 
        {
            GameObject.DestroyImmediate(planet.transform.GetChild( i ).gameObject);
        }
        
    }

    /// <summary>
    /// init 6 faces of the planet
    /// </summary>
    void InitGrid()
    {
        planetData.posCamStart = planet.transform.position + (planet.transform.up * (1 + planetSettings.radius + planetSettings.height+2));
        planetData = planet.AddComponent<GridPlanetData>();
        // set up the list Faces
        planetData.FaceNord = new List<GridTile>();
        planetData.FaceEst = new List<GridTile>();
        planetData.FaceOuest = new List<GridTile>();
        planetData.FaceFront = new List<GridTile>();
        planetData.FaceBack = new List<GridTile>();
        planetData.FaceSud = new List<GridTile>();

        // add to list grid
        planetData.listGridFaces = new List<List<GridTile>>();
        planetData.listGridFaces.Add(planetData.FaceNord);
        planetData.listGridFaces.Add(planetData.FaceEst);
        planetData.listGridFaces.Add(planetData.FaceOuest);
        planetData.listGridFaces.Add(planetData.FaceFront);
        planetData.listGridFaces.Add(planetData.FaceBack);
        planetData.listGridFaces.Add(planetData.FaceSud);

        // get pos and rad planet for convinience
        Vector3 pos = planet.transform.position;
        float rad =  planetSettings.radius;

        // create the base cube points
        // each point is named after the 3 face it's composed of
        planetData._pointNordOuestBack = new Vector3(pos.x+rad, pos.y+rad, pos.z-rad);
        planetData._pointNordOuestFront = new Vector3(pos.x+rad, pos.y+rad, pos.z+rad);
        planetData._pointNordEstBack = new Vector3(pos.x-rad, pos.y+rad, pos.z-rad);
        planetData._pointNordEstFront = new Vector3(pos.x-rad, pos.y+rad, pos.z+rad);
        planetData._pointSudOuestBack = new Vector3(pos.x+rad, pos.y-rad, pos.z-rad);
        planetData._pointSudOuestFront = new Vector3(pos.x+rad, pos.y-rad, pos.z+rad);
        planetData._pointSudEstBack = new Vector3(pos.x-rad, pos.y-rad, pos.z-rad);
        planetData._pointSudEstFront = new Vector3(pos.x-rad, pos.y-rad, pos.z+rad);

        // create the planet faces if enable
        if(planetSettings.faceUp){
            // face Nord
            //       ^ Back
            //     +---+
            //Est< |   | > Ouest
            //     +---+
            //      \/ Front
            InitFaceGrid(0, planetData._pointNordEstBack, planetData._pointNordOuestBack, planetData._pointNordEstFront, planetData._pointNordOuestFront, planetData.FaceNord);
        }
        if(planetSettings.faceRight){
            // face Est
            //         ^ Nord
            //       +---+
            //Front< |   | > Back
            //       +---+
            //        \/ Sud
            InitFaceGrid(1, planetData._pointNordEstFront, planetData._pointNordEstBack, planetData._pointSudEstFront, planetData._pointSudEstBack, planetData.FaceEst);
        }
        if(planetSettings.faceLeft){
            // face Ouest
            //         ^ Nord
            //       +---+
            // Back< |   | > Front
            //       +---+
            //        \/ Sud
            InitFaceGrid(2, planetData._pointNordOuestBack, planetData._pointNordOuestFront, planetData._pointSudOuestBack, planetData._pointSudOuestFront, planetData.FaceOuest);
        }
        if(planetSettings.faceForward){
            // face Front
            //         ^ Nord
            //       +---+
            //Ouest< |   | > Est
            //       +---+
            //        \/ Sud
            InitFaceGrid(3, planetData._pointNordOuestFront, planetData._pointNordEstFront, planetData._pointSudOuestFront, planetData._pointSudEstFront, planetData.FaceFront);
        }
        if(planetSettings.faceBack){
            // face Back
            //       ^ Nord
            //     +---+
            //Est< |   | > Ouest
            //     +---+
            //      \/ Sud
            InitFaceGrid(4, planetData._pointNordEstBack, planetData._pointNordOuestBack, planetData._pointSudEstBack, planetData._pointSudOuestBack, planetData.FaceBack);
        }
        if(planetSettings.faceDown){
            // face Sud
            //       ^ Back
            //     +---+
            //Est< |   | > Ouest
            //     +---+
            //      \/ Front
            InitFaceGrid(5, planetData._pointSudEstBack, planetData._pointSudOuestBack, planetData._pointSudEstFront, planetData._pointSudOuestFront, planetData.FaceSud);
        }
    }

    /// <summary>
    /// init all Tile of A face
    /// </summary>
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
                    NO = Vector3.Lerp(IntermedNord0, IntermedSud0, (float)N/(float)planetSettings.resolution) * (float)(heightTileMulti);
                    NE = Vector3.Lerp(IntermedNord1, IntermedSud1, (float)N/(float)planetSettings.resolution) * (float)(heightTileMulti);
                    SO = Vector3.Lerp(IntermedNord0, IntermedSud0, (float)(N+1)/(float)planetSettings.resolution) * (float)(heightTileMulti);
                    SE = Vector3.Lerp(IntermedNord1, IntermedSud1, (float)(N+1)/(float)planetSettings.resolution) * (float)(heightTileMulti);

                    NOH = NO * (float)(heightTileMulti + planetSettings.tileHeight);
                    NEH = NE * (float)(heightTileMulti + planetSettings.tileHeight);
                    SOH = SO * (float)(heightTileMulti + planetSettings.tileHeight);
                    SEH = SE * (float)(heightTileMulti + planetSettings.tileHeight);

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
                    GridTile t = new GridTile(numFace, N, O, (planetSettings.resolution-O-1), (planetSettings.resolution-N-1), h, planetSettings.height-1-h);
                    t.InitSquare(NO, NE, SO, SE, NOH, NEH, SOH, SEH);
                    t.tileGameObject.transform.parent = planet.transform;
                    switch (numFace)
                    {
                        case 0:
                        planetData.FaceNord.Add(t);
                        break;
                        
                        case 1:
                        planetData.FaceEst.Add(t);
                        break;

                        case 2:
                        planetData.FaceOuest.Add(t);
                        break;

                        case 3:
                        planetData.FaceFront.Add(t);
                        break;

                        case 4:
                        planetData.FaceBack.Add(t);
                        break;

                        case 5:
                        planetData.FaceSud.Add(t);
                        break;

                        default:
                        break;
                    }
                }
            }
            heightTileMulti = heightTileMulti + planetSettings.tileHeight;
        }
    }

    /// <summary>
    /// Call Every Tile to set their proxy
    /// </summary>
    public void InitProxyTile()
    {
        // Nord
        foreach (GridTile tile in planetData.FaceNord)
        {
            tile.SetProxyTileGrid(planetData.listGridFaces);
        }
        // Est
        foreach (GridTile tile in planetData.FaceEst)
        {
            tile.SetProxyTileGrid(planetData.listGridFaces);
        }
        // Ouest
        foreach (GridTile tile in planetData.FaceOuest)
        {
            tile.SetProxyTileGrid(planetData.listGridFaces);
        }
        // Front
        foreach (GridTile tile in planetData.FaceFront)
        {
            tile.SetProxyTileGrid(planetData.listGridFaces);
        }
        // Back
        foreach (GridTile tile in planetData.FaceBack)
        {
            tile.SetProxyTileGrid(planetData.listGridFaces);
        }
        // Sud
        foreach (GridTile tile in planetData.FaceSud)
        {
            tile.SetProxyTileGrid(planetData.listGridFaces);
        }
    }

    /// <summary>
    /// Call Every Tile to set Tile Manager
    /// </summary>
    public void SetTilesManager()
    {
        // Nord
        foreach (GridTile tile in planetData.FaceNord)
        {
            tile.SetTileManager();
        }
        // Est
        foreach (GridTile tile in planetData.FaceEst)
        {
            tile.SetTileManager();
        }
        // Ouest
        foreach (GridTile tile in planetData.FaceOuest)
        {
            tile.SetTileManager();
        }
        // Front
        foreach (GridTile tile in planetData.FaceFront)
        {
            tile.SetTileManager();
        }
        // Back
        foreach (GridTile tile in planetData.FaceBack)
        {
            tile.SetTileManager();
        }
        // Sud
        foreach (GridTile tile in planetData.FaceSud)
        {
            tile.SetTileManager();
        }
    }

    /// <summary>
    /// Call Every Tile to set various stuff
    /// </summary>
    public void InitTileGridGameObject()
    {
        // Nord
        foreach (GridTile tile in planetData.FaceNord)
        {
            tile.InitTileGridGameObject();
        }
        // Est
        foreach (GridTile tile in planetData.FaceEst)
        {
            tile.InitTileGridGameObject();
        }
        // Ouest
        foreach (GridTile tile in planetData.FaceOuest)
        {
            tile.InitTileGridGameObject();
        }
        // Front
        foreach (GridTile tile in planetData.FaceFront)
        {
            tile.InitTileGridGameObject();
        }
        // Back
        foreach (GridTile tile in planetData.FaceBack)
        {
            tile.InitTileGridGameObject();
        }
        // Sud
        foreach (GridTile tile in planetData.FaceSud)
        {
            tile.InitTileGridGameObject();
        }
    }
}
