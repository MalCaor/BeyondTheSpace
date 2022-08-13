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
    }

    public void SetProxyTileGrid(List<List<GridTile>> gridGlob)
    {
        if(Npos!=0 && Opos!=0 && Epos!=0 && Spos!=0)
        {
            //gridGlob[face]
            ProxyTileNord = gridGlob[face].Find((x) => x.Npos==Npos-1 && x.Opos==Opos && x.Dpos==Dpos);
            ProxyTileSud = gridGlob[face].Find((x) => x.Npos==Npos+1 && x.Opos==Opos && x.Dpos==Dpos);
            ProxyTileOuest = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos-1 && x.Dpos==Dpos);
            ProxyTileEst = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos+1 && x.Dpos==Dpos);
            ProxyTileUp = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos && x.Dpos==Dpos-1);
            ProxyTileDown = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos && x.Dpos==Dpos+1);
        } else {
            // need to go on other face 
            // first num is facenum ( Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5 )
            //second is direction 0 = Up / 1 = Right / 2 = Down / 3 = Left
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
            if(Npos == 0){
                
            } else {
                ProxyTileNord = gridGlob[face].Find((x) => x.Npos==Npos-1 && x.Opos==Opos && x.Dpos==Dpos);
            }
            if(Spos == 0){

            } else {
                ProxyTileSud = gridGlob[face].Find((x) => x.Npos==Npos+1 && x.Opos==Opos && x.Dpos==Dpos);
            }
            if(Opos == 0){

            } else {
                ProxyTileOuest = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos-1 && x.Dpos==Dpos);
            }
            if(Epos == 0){

            } else {
                ProxyTileEst = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos+1 && x.Dpos==Dpos);
            }
            // up and down like normal (will retrun null if can't find)
            ProxyTileUp = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos && x.Dpos==Dpos-1);
            ProxyTileDown = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos && x.Dpos==Dpos+1);
        }
    }
}
