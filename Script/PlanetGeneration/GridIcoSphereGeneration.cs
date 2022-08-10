using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GridIcoSphereGeneration : MonoBehaviour
{
    public PlanetGenerationSettings planetSettings;
    public List<GameObject> grid;

    public void InitGrid()
    {
        // destroy the child
        DestroyChild();

        // init the list
        grid = new List<GameObject>();

        // create 12 vertices of a icosahedron
        float t = (float)((1.0 + Math.Sqrt(5.0)) / 2.0);
        // test prefab Prefab/Test/CubePointTest
        string prefabName = "";
        if(planetSettings.PointTest)
        {
            // load test prefab
            prefabName = "Prefab/Test/CubePointTest";
        }else{
            // load real point
            prefabName = "Prefab/Grid/GridPoint";
        }

        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3(-1,  t,  0));
        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 1,  t,  0));
        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3(-1, -t,  0));
        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 1, -t,  0));

        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 0, -1,  t));
        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 0,  1,  t));
        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 0, -1, -t));
        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 0,  1, -t));

        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( t,  0, -1));
        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( t,  0,  1));
        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3(-t,  0, -1));
        grid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        grid.LastOrDefault().transform.parent = transform;
        grid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3(-t,  0,  1));
    }

    /// <summary>
    /// Destroy all point
    /// </summary>
    void DestroyChild()
    {
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--) 
        {
            GameObject.DestroyImmediate(transform.GetChild( i ).gameObject);
        }
    }
}
