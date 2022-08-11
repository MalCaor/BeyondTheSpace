using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlanetGeneration : MonoBehaviour
{
    // vars Pub
    
    public PlanetGenerationSettings planetSettings;

   /// <summary>
    /// Init The Grid
    /// </summary>
    public void Init()
    {
        DestroyChild();
        InitGrid();
        // insert wait for Init To finish before continues
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

    /// <summary>
    /// init 6 faces of the planet
    /// </summary>
    void InitGrid()
    {
        
    }

    
}
