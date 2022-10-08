using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary> 
/// Generate the Terrain of a planet from 6 bioms maps.<br/>
/// </Summary>
public class TerrainPlanetGeneration : MonoBehaviour
{
    /// <summary> planet GameObjet target of the generation </summary>
    public GameObject planet;
    [HideInInspector]
    /// <summary> Grid data that will store the result of the generation </summary>
    public GridPlanetData grid;
    [HideInInspector]
    /// <summary> access some vars from the GridPlanetGeneration </summary>
    public GridPlanetGeneration gridGener;

    /// <summary> Perlin Noize </summary>
    Noise noise;

    /// <summary> Planet Terrain Generation Settings </summary>
    public TerrainPlanetGenerationSetting terrainSetting;

    /// <Summary> 
    /// Init the Generation.<br/>
    /// </Summary>
    public void Init()
    {
        noise = new Noise(terrainSetting.SeedTerrainGeneration);
        grid = planet.GetComponent<GridPlanetData>();
        gridGener = gameObject.GetComponent<GridPlanetGeneration>();
        // init all env
        EnvironmentGlobalGeneration.InitAllEnvironment();
        InitTerrain();
    }

    /// <Summary> 
    /// Init the terrain for each tile in each face.<br/>
    /// </Summary>
    private void InitTerrain()
    {
        // Nord
        foreach (GridTile tile in grid.FaceNord)
        {
            InitTileTerrain(tile);
        }
        // Est
        foreach (GridTile tile in grid.FaceEst)
        {
            InitTileTerrain(tile);
        }
        // Ouest
        foreach (GridTile tile in grid.FaceOuest)
        {
            InitTileTerrain(tile);
        }
        // Front
        foreach (GridTile tile in grid.FaceFront)
        {
            InitTileTerrain(tile);
        }
        // Back
        foreach (GridTile tile in grid.FaceBack)
        {
            InitTileTerrain(tile);
        }
        // Sud
        foreach (GridTile tile in grid.FaceSud)
        {
            InitTileTerrain(tile);
        }
    }

    void InitTileTerrain(GridTile t)
    {
        /// TODO : GENERATE Terrain per grid from Biom Pixel
    }
}
