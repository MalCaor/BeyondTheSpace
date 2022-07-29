using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlanetGeneration : MonoBehaviour
{
    // vars Pub
    // rayon
    public float rayonVoid;
    public float rayonOrbite;
    public float resolution = 1f;

    // vars Priv
    // List cube ?
    List<GameObject> listCubeGrid;

    // Start is called before the first frame update
    void Start()
    {
        // Generate grid
        generateGrid();
    }

    void generateGrid()
    {
        // init
        listCubeGrid = new List<GameObject>();
    }
}
