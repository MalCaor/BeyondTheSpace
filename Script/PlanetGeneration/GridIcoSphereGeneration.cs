using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GridIcoSphereGeneration : MonoBehaviour
{
    // struct
    [System.Serializable]
    public struct LayerGrid<GameObject>
    {
        [SerializeField]
        public List<GameObject> layerGrid;
    }

    public PlanetGenerationSettings planetSettings;
    public List<LayerGrid<GameObject>> grid;

    public void InitGrid()
    {
        // destroy the child
        DestroyChild();

        // init the list
        grid = new List<LayerGrid<GameObject>>();

        for (int h = 0; h < planetSettings.height; h++)
        {
            grid.Add(new LayerGrid<GameObject>());
            GenerateGridLayer(grid[h], h);
        }
    }

    public void GenerateGridLayer(LayerGrid<GameObject> layer, int layerLevel)
    {
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

        float min = -1 * (planetSettings.radius);
        float max = 1 * (planetSettings.radius);

        layer.layerGrid = new List<GameObject>();

        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3(min,  t,  0));
        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( max,  t,  0));
        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3(min, -t,  0));
        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( max, -t,  0));

        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 0, min,  t));
        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 0,  max,  t));
        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 0, min, -t));
        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( 0,  max, -t));

        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( t,  0, min));
        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3( t,  0,  max));
        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3(-t,  0, min));
        layer.layerGrid.Add(Instantiate(Resources.Load<GameObject>(prefabName)) as GameObject);
        layer.layerGrid.LastOrDefault().transform.parent = transform;
        layer.layerGrid.LastOrDefault().GetComponent<GridPointIco>().InitPoint(new Vector3(-t,  0,  max));
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
