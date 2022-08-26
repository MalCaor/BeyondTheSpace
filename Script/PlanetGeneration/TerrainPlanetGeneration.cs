using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPlanetGeneration : MonoBehaviour
{
    // grid
    GridPlanetGeneration grid;

    // setting
    public TerrainPlanetGenerationSetting terrainSetting;

    public void Init()
    {
        grid = gameObject.GetComponent<GridPlanetGeneration>();
        InitTerrain();
    }

    void InitTerrain()
    {

    }
}
