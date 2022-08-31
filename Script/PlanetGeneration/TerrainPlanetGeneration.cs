using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPlanetGeneration : MonoBehaviour
{
    // grid
    public GameObject planet;
    [HideInInspector]
    public GridPlanetData grid;
    [HideInInspector]
    public GridPlanetGeneration gridGener;

    // noise
    Noise noise;

    // setting
    public TerrainPlanetGenerationSetting terrainSetting;

    public void Init()
    {
        noise = new Noise(terrainSetting.SeedTerrainGeneration);
        grid = planet.GetComponent<GridPlanetData>();
        gridGener = gameObject.GetComponent<GridPlanetGeneration>();
        // init all env
        EnvironmentGlobalGeneration.InitAllEnvironment();
        InitTerrain();
        if (terrainSetting.water)
        {
            InitWater();
        }
        DrawGameObject();
    }

    void InitTerrain()
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

    void InitWater()
    {
        // Nord
        foreach (GridTile tile in grid.FaceNord)
        {
            InitTileWater(tile);
        }
        // Est
        foreach (GridTile tile in grid.FaceEst)
        {
            InitTileWater(tile);
        }
        // Ouest
        foreach (GridTile tile in grid.FaceOuest)
        {
            InitTileWater(tile);
        }
        // Front
        foreach (GridTile tile in grid.FaceFront)
        {
            InitTileWater(tile);
        }
        // Back
        foreach (GridTile tile in grid.FaceBack)
        {
            InitTileWater(tile);
        }
        // Sud
        foreach (GridTile tile in grid.FaceSud)
        {
            InitTileWater(tile);
        }
    }

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
    }

    void InitTileTerrain(GridTile t)
    {
        // generate terrain
        // get angle from planet
        // pointDownNO is just to have a point to focus on
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
    }

    void InitTileWater(GridTile t)
    {
        if(t.Dpos<=terrainSetting.waterLevel && t.gridTileManager.tileTerrainType == 0)
        {
            t.InitTileGridGameObject();
            t.gridTileManager.tileTerrainType = 2;
            t.gridTileManager.environmentManager.listEnvironment.Add(Environment.listEnvironmentGlobal.Find((x) => x.name=="water"));
        }
    }

    void DrawGameObjectTerrain(GridTile t)
    {
        if(t.tileGameObject!=null)
        {
            t.tileGameObject.GetComponent<GridTileGameObject>().DrawGameObject();
        }
    }
}
