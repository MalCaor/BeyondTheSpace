using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridTile
{
    // name for the player
    public string name;

    // GameObject in world
    public GameObject tile;

    // Proxy GridTile
    public GridTile ProxyTileNord;
    public GridTile ProxyTileSud;
    public GridTile ProxyTileOuest;
    public GridTile ProxyTileEst;
    public GridTile ProxyTileUp;
    public GridTile ProxyTileDown;

    // Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5
    int face;

    // position in face grid
    // if 0 the tile is the first one, if = max then it's on the other side
    int Npos;
    int Opos;
    int Epos;
    int Spos;
    int Upos;
    int Dpos;

    // point that delimite the tile in a 3d space
    // points down
    // NO = NordOuest / NE NordEst / SE SudEst / SO SudOuest
    Vector3 pointDownNO;
    Vector3 pointDownNE;
    Vector3 pointDownSO;
    Vector3 pointDownSE;
    // points up
    Vector3 pointUpNO;
    Vector3 pointUpNE;
    Vector3 pointUpSO;
    Vector3 pointUpSE;

    // constructor
    public GridTile(int numFace, int N, int O, int E, int S, int U, int D)
    {
        // set num Face ( Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5 )
        this.face = numFace;
        // set pos in grid
        this.Npos = N;
        this.Opos = O;
        this.Epos = E;
        this.Spos = S;
        this.Upos = U;
        this.Dpos = D;

        this.name = "Tile Face " + face + " : " + Npos + ", " + Opos + ", " + Epos + ", " + Spos + ", " + Dpos;
    }

    // set square
    public void InitSquare(Vector3 NO, Vector3 NE, Vector3 SO, Vector3 SE, Vector3 NOH, Vector3 NEH, Vector3 SOH, Vector3 SEH)
    {
        pointDownNO = NO;
        pointDownNE = NE;
        pointDownSO = SO;
        pointDownSE = SE;

        pointUpNO = NOH;
        pointUpNE = NEH;
        pointUpSO = SOH;
        pointUpSE = SEH;

        // set the grid
        tile = new GameObject();
        tile.name = this.name;
        LineRenderer lDown = tile.AddComponent<LineRenderer>();
        lDown.startColor = Color.black;
        lDown.endColor = Color.black;
        lDown.startWidth = 0.01f;
        lDown.endWidth = 0.01f;
        lDown.positionCount = 10;
        lDown.SetPosition(0, pointDownNO);
        lDown.SetPosition(1, pointDownNE);
        lDown.SetPosition(2, pointDownSE);
        lDown.SetPosition(3, pointDownSO);
        lDown.SetPosition(4, pointDownNO);
        // up
        lDown.SetPosition(5, pointUpNO);
        lDown.SetPosition(6, pointUpNE);
        lDown.SetPosition(7, pointUpSE);
        lDown.SetPosition(8, pointUpSO);
        lDown.SetPosition(9, pointUpNO);

        // set Link to object in GameObject
        GridTileGameObject linkToThis = tile.AddComponent<GridTileGameObject>();
        linkToThis.gridTile = this;
    }

    public void SetProxyTileGrid(List<List<GridTile>> gridGlob)
    {
        // call findTileOtherFace for each Proxy
        ProxyTileNord = findTileOtherFace(gridGlob, 0);
        ProxyTileSud = findTileOtherFace(gridGlob, 1);
        ProxyTileOuest = findTileOtherFace(gridGlob, 2);
        ProxyTileEst = findTileOtherFace(gridGlob, 3);
        ProxyTileUp = findTileOtherFace(gridGlob, 4);
        ProxyTileDown = findTileOtherFace(gridGlob, 5);

        // Update the proxy for GameObject
        tile.GetComponent<GridTileGameObject>().UpdateProxy();
    }

    /// <summary>
    /// Find a Tile even on the other Face
    /// dirTile = direction ( Nord 0 / Est 1 / Ouest 2 / Sud 3 / Up 4 / Down 5 )
    /// </summary>
    GridTile findTileOtherFace(List<List<GridTile>> gridGlob, int dirTile)
    {
        // up and down like normal (will retrun null if can't find)
        if(dirTile == 4)
        {
            // Up (4)
            return gridGlob[this.face].Find((x) => x.Npos==this.Npos && x.Opos==this.Opos && x.Dpos==this.Dpos+1);
        }
        if(dirTile == 5)
        {
            // Down (5)
            return gridGlob[this.face].Find((x) => x.Npos==this.Npos && x.Opos==this.Opos && x.Dpos==this.Dpos-1);
        }
        
        // Will need to find Tile on other "column"

        // first num is facenum ( Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5 )
        // second is direction 0 = Up / 1 = Right / 2 = Down / 3 = Left
        int[,] linkFaceToFace = new int[6,4];
        // face Nord
        //       ^ Back
        //     +---+
        //Est< |   | > Ouest
        //     +---+
        //      \/ Front
        linkFaceToFace[0, 0] = 4;
        linkFaceToFace[0, 1] = 2;
        linkFaceToFace[0, 2] = 3;
        linkFaceToFace[0, 3] = 1;
        // face Est
        //        ^ Nord
        //      +---+
        //Back< |   | > Front
        //      +---+
        //       \/ Sud
        linkFaceToFace[1, 0] = 0;
        linkFaceToFace[1, 1] = 3;
        linkFaceToFace[1, 2] = 5;
        linkFaceToFace[1, 3] = 4;
        // face Ouest
        //         ^ Nord
        //       +---+
        //Front< |   | > Back
        //       +---+
        //        \/ Sud
        linkFaceToFace[2, 0] = 0;
        linkFaceToFace[2, 1] = 4;
        linkFaceToFace[2, 2] = 5;
        linkFaceToFace[2, 3] = 3;
        // face Front
        //         ^ Nord
        //       +---+
        //Ouest< |   | > Est
        //       +---+
        //        \/ Sud
        linkFaceToFace[3, 0] = 0;
        linkFaceToFace[3, 1] = 1;
        linkFaceToFace[3, 2] = 5;
        linkFaceToFace[3, 3] = 2;
        // face Back
        //       ^ Nord
        //     +---+
        //Est< |   | > Ouest
        //     +---+
        //      \/ Sud
        linkFaceToFace[4, 0] = 0;
        linkFaceToFace[4, 1] = 2;
        linkFaceToFace[4, 2] = 5;
        linkFaceToFace[4, 3] = 1;
        // face Sud
        //       ^ Back
        //     +---+
        //Est< |   | > Ouest
        //     +---+
        //      \/ Front
        linkFaceToFace[5, 0] = 4;
        linkFaceToFace[5, 1] = 2;
        linkFaceToFace[5, 2] = 3;
        linkFaceToFace[5, 3] = 1;
        
        // now find proxy
        if(dirTile == 0){
            if(Npos == 0){
                // get the grid where the face is
                List<GridTile> grid = gridGlob[linkFaceToFace[this.face, 0]];
                
            } else {
                return gridGlob[face].Find((x) => x.Npos==Npos-1 && x.Opos==Opos && x.Dpos==Dpos);
            }
        }
        if(dirTile == 3){
            if(Spos == 0){
                // get the grid where the face is
                List<GridTile> grid = gridGlob[linkFaceToFace[this.face, 0]];
            } else {
                return gridGlob[face].Find((x) => x.Npos==Npos+1 && x.Opos==Opos && x.Dpos==Dpos);
            }
        }
        if(dirTile == 2){
            if(Opos == 0){
                // get the grid where the face is
                List<GridTile> grid = gridGlob[linkFaceToFace[this.face, 0]];
            } else {
                return gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos-1 && x.Dpos==Dpos);
            }
        }
        if(dirTile == 1){
            if(Epos == 0){
                // get the grid where the face is
                List<GridTile> grid = gridGlob[linkFaceToFace[this.face, 0]];
            } else {
                return gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos+1 && x.Dpos==Dpos);
            }
        }

        // Error
        Debug.Log("Error Proxy Tile Atribution :  Tile " + this.name);
        return null;
    }
}
