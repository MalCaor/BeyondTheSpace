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

    /// <summary> Text map of Bioms Face Nord </summary>
    Texture2D mapFaceNord;
    /// <summary> Text map of Bioms Face Est </summary>
    Texture2D mapFaceEst;
    /// <summary> Text map of Bioms Face Ouest </summary>
    Texture2D mapFaceOuest;
    /// <summary> Text map of Bioms Face Front </summary>
    Texture2D mapFaceFront;
    /// <summary> Text map of Bioms Face Back </summary>
    Texture2D mapFaceBack;
    /// <summary> Text map of Bioms Face Sud </summary>
    Texture2D mapFaceSud;

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
            InitTileTerrain(tile, mapFaceNord);
        }
        // Est
        foreach (GridTile tile in grid.FaceEst)
        {
            InitTileTerrain(tile, mapFaceEst);
        }
        // Ouest
        foreach (GridTile tile in grid.FaceOuest)
        {
            InitTileTerrain(tile, mapFaceOuest);
        }
        // Front
        foreach (GridTile tile in grid.FaceFront)
        {
            InitTileTerrain(tile, mapFaceFront);
        }
        // Back
        foreach (GridTile tile in grid.FaceBack)
        {
            InitTileTerrain(tile, mapFaceBack);
        }
        // Sud
        foreach (GridTile tile in grid.FaceSud)
        {
            InitTileTerrain(tile, mapFaceSud);
        }
    }

    /// <Summary> 
    /// Init the Terrain (and optional building) of a tile with the biom map.<br/>
    /// </Summary>
    /// <param name="gridTile">gridTile to be init.</param>
    /// <param name="map">biom map of the tile.</param>
    void InitTileTerrain(GridTile gridTile, Texture2D map)
    {
        /// TODO : GENERATE Terrain per grid from Biom Pixel
        
        // get color to the tile pos
        Color col = map.GetPixel(gridTile.Npos, gridTile.Epos);
        if(col == null) throw new System.Exception("map index null, check if the Biom map is the right size");

        // get the biom
        Biom biom = Biom.GetBiom(col);
        if(biom == null) {
            // couldn't find a biom so put the default one instead
            Debug.LogError("Color not matching any biom");
            biom = Biom.GetBiom(Color.black);
        }
    }
}
