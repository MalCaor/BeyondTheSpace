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

    /* OLD CODE ONLY KEEP AS BACK UP
    void DrawGameObject()
    {
        // Nord
        foreach (GridTile tile in grid.FaceNord)
        {
            DrawGameObjectTerrain(tile);
        }
        // Est
        foreach (GridTile tile in grid.FaceEst)
        {
            DrawGameObjectTerrain(tile);
        }
        // Ouest
        foreach (GridTile tile in grid.FaceOuest)
        {
            DrawGameObjectTerrain(tile);
        }
        // Front
        foreach (GridTile tile in grid.FaceFront)
        {
            DrawGameObjectTerrain(tile);
        }
        // Back
        foreach (GridTile tile in grid.FaceBack)
        {
            DrawGameObjectTerrain(tile);
        }
        // Sud
        foreach (GridTile tile in grid.FaceSud)
        {
            DrawGameObjectTerrain(tile);
        }
    }*/

    void InitTileTerrain(GridTile t)
    {
        /// OLD CODE ONLY KEEP AS BACK UP
        // generate terrain
        // get angle from planet
        // pointDownNO is just to have a point to focus on
        /*
        float angleUp = Vector3.Angle(planet.transform.up, t.pointDownNO - planet.transform.position);
        float angleRight = Vector3.Angle(planet.transform.right, t.pointDownNO - planet.transform.position);
        // evaluate
        float noiseVal = (noise.Evaluate(new Vector4(angleUp, t.Npos, angleRight, t.Opos) / terrainSetting.roughnessNoiseTerrainElevation)+1) * 0.5f;
        int levelTerrain = (int)Mathf.Round(gridGener.planetSettings.height * noiseVal);
        if(t.Dpos>levelTerrain)
        {
            t.gridTileManager.tileTerrainType = 0;
            t.gridTileManager.environmentManager.listEnvironment.Add(Environment.listEnvironmentGlobal.Find((x) => x.name=="air"));
        } else {
            t.InitTileGridGameObject();
            t.gridTileManager.tileTerrainType = 1;
            t.gridTileManager.environmentManager.listEnvironment.Add(Environment.listEnvironmentGlobal.Find((x) => x.name=="earth"));;
        }
        */

        /// TODO : GENERATE Terrain per grid from Biom Pixel
    }

    /*    OLD CODE ONLY KEEP AS BACK UP
    void InitTileWater(GridTile t)
    {
        if(t.Dpos<=terrainSetting.waterLevel && t.gridTileManager.tileTerrainType == 0)
        {
            t.InitTileGridGameObject();
            t.gridTileManager.tileTerrainType = 2;
            t.gridTileManager.environmentManager.listEnvironment.Add(Environment.listEnvironmentGlobal.Find((x) => x.name=="water"));
        }
    }*/

    /* OLD CODE ONLY KEEP AS BACK UP
    void DrawGameObjectTerrain(GridTile t)
    {
        if(t.tileGameObject!=null)
        {
            t.tileGameObject.GetComponent<GridTileGameObject>().DrawGameObject();
        }
    }
    */
}
