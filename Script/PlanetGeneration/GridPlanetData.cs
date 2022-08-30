using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlanetData : MonoBehaviour
{
    // the 6 faces of a planet
    [HideInInspector]
    public List<GridTile> FaceNord;
    [HideInInspector]
    public List<GridTile> FaceEst;
    [HideInInspector]
    public List<GridTile> FaceOuest;
    [HideInInspector]
    public List<GridTile> FaceFront;
    [HideInInspector]
    public List<GridTile> FaceBack;
    [HideInInspector]
    public List<GridTile> FaceSud;

    // here for easy acces
    public List<List<GridTile>> listGridFaces;

    // vect
    public Vector3 _pointNordOuestBack;
    public Vector3 _pointNordOuestFront;
    public Vector3 _pointNordEstBack;
    public Vector3 _pointNordEstFront;
    public Vector3 _pointSudOuestBack;
    public Vector3 _pointSudOuestFront;
    public Vector3 _pointSudEstBack;
    public Vector3 _pointSudEstFront;
}
