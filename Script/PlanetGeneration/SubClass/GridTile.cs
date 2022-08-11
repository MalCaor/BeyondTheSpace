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

    // Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5
    int face;

    // position in face grid
    // if 0 the tile is the first one, if = max then it's on the other side
    int Npos;
    int Opos;
    int Epos;
    int Spos;

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
    public GridTile(int numFace, int N, int O, int E, int S)
    {
        // set num Face ( Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5 )
        this.face = numFace;
        // set pos in grid
        this.Npos = N;
        this.Opos = O;
        this.Epos = E;
        this.Spos = S;

        this.name = "Tile Face " + face + " : " + Npos + ", " + Opos + ", " + Epos + ", " + Spos;
    }

    // set square
    public void InitSquare(Vector3 NO, Vector3 NE, Vector3 SO, Vector3 SE)
    {
        pointDownNO = NO;
        pointDownNE = NE;
        pointDownSO = SO;
        pointDownSE = SE;

        // set the grid
        tile = new GameObject();
        tile.name = this.name;
        LineRenderer lDown = tile.AddComponent<LineRenderer>();
        lDown.startColor = Color.black;
        lDown.endColor = Color.black;
        lDown.startWidth = 0.01f;
        lDown.endWidth = 0.01f;
        lDown.positionCount = 5;
        lDown.SetPosition(0, pointDownNO);
        lDown.SetPosition(1, pointDownNE);
        lDown.SetPosition(2, pointDownSE);
        lDown.SetPosition(3, pointDownSO);
        lDown.SetPosition(4, pointDownNO);
    }
}
