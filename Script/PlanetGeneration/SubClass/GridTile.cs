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
        if(Npos!=0 && Opos!=0 && Epos!=0 && Spos!=0 && Upos!=0 && Dpos!=0)
        {
            //gridGlob[face]
            ProxyTileNord = gridGlob[face].Find((x) => x.Npos==Npos-1 && x.Opos==Opos && x.Dpos==Dpos);
            ProxyTileSud = gridGlob[face].Find((x) => x.Npos==Npos+1 && x.Opos==Opos && x.Dpos==Dpos);
            ProxyTileOuest = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos-1 && x.Dpos==Dpos);
            ProxyTileEst = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos+1 && x.Dpos==Dpos);
            ProxyTileUp = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos && x.Dpos==Dpos-1);
            ProxyTileDown = gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos && x.Dpos==Dpos+1);
        }
    }
}
