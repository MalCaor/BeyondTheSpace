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
    // List Points grid
    List<Vector3> listPointGrid;

    // Start is called before the first frame update
    void Start()
    {
        // Generate grid
        generateGrid();
    }

    void generateGrid()
    {
        // init
        listPointGrid = new List<Vector3>();
        // parcour
        for (float x = transform.localPosition.x - rayonOrbite; x < transform.localPosition.x + rayonOrbite; x+=resolution)
        {
            for (float y = transform.localPosition.y - rayonOrbite; y < transform.localPosition.y + rayonOrbite; y+=resolution)
            {
                for (float z = transform.localPosition.z - rayonOrbite; z < transform.localPosition.z + rayonOrbite; z+=resolution)
                {
                    // add the point to the list
                    listPointGrid.Add(new Vector3(x, y, z));
                }
            }
        }
    }
}
