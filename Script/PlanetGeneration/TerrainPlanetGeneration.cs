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
    Noise noise = new Noise();

    // setting
    public TerrainPlanetGenerationSetting terrainSetting;

    public void Init()
    {
        grid = planet.GetComponent<GridPlanetData>();
        gridGener = gameObject.GetComponent<GridPlanetGeneration>();
        // init all env
        EnvironmentGlobalGeneration.InitAllEnvironment();
        InitTerrain();
        ResetColor();
        if (terrainSetting.water)
        {
            InitWater();
        }
        SetColor();
        DrawGameObject();
    }

    void ResetColor()
    {
        // Nord
        foreach (GridTile tile in grid.FaceNord)
        {
            ResetColorTile(tile);
        }
        // Est
        foreach (GridTile tile in grid.FaceEst)
        {
            ResetColorTile(tile);
        }
        // Ouest
        foreach (GridTile tile in grid.FaceOuest)
        {
            ResetColorTile(tile);
        }
        // Front
        foreach (GridTile tile in grid.FaceFront)
        {
            ResetColorTile(tile);
        }
        // Back
        foreach (GridTile tile in grid.FaceBack)
        {
            ResetColorTile(tile);
        }
        // Sud
        foreach (GridTile tile in grid.FaceSud)
        {
            ResetColorTile(tile);
        }
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

    void SetColor()
    {
        // Nord
        foreach (GridTile tile in grid.FaceNord)
        {
            SetColorTile(tile);
        }
        // Est
        foreach (GridTile tile in grid.FaceEst)
        {
            SetColorTile(tile);
        }
        // Ouest
        foreach (GridTile tile in grid.FaceOuest)
        {
            SetColorTile(tile);
        }
        // Front
        foreach (GridTile tile in grid.FaceFront)
        {
            SetColorTile(tile);
        }
        // Back
        foreach (GridTile tile in grid.FaceBack)
        {
            SetColorTile(tile);
        }
        // Sud
        foreach (GridTile tile in grid.FaceSud)
        {
            SetColorTile(tile);
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

    void ResetColorTile(GridTile t)
    {
        t.tileGameObject.GetComponent<GridTileGameObject>().SetLineColor(Color.black);
    }

    void InitTileTerrain(GridTile t)
    {
        // generate terrain
        // get angle from top planet
        float angleUp = Vector3.Angle(planet.transform.up, t.tileGameObject.transform.position - planet.transform.position);
        float angleRight = Vector3.Angle(planet.transform.right, t.tileGameObject.transform.position - planet.transform.position);
        float angleLeft = Vector3.Angle(-planet.transform.right, t.tileGameObject.transform.position - planet.transform.position);
        float noiseVal = (noise.Evaluate(new Vector3(angleUp, angleRight, angleLeft) / terrainSetting.roughnessNoiseTerrainElevation)+1) * 0.5f;
        int levelTerrain = (int)Mathf.Round(gridGener.planetSettings.height * noiseVal);
        if(t.Dpos>levelTerrain)
        {
            t.gridTileManager.tileTerrainType = 0;
            t.gridTileManager.environmentManager.listEnvironment.Add(Environment.listEnvironmentGlobal.Find((x) => x.name=="air"));
        } else {
            t.gridTileManager.tileTerrainType = 1;
            t.gridTileManager.environmentManager.listEnvironment.Add(Environment.listEnvironmentGlobal.Find((x) => x.name=="earth"));;
        }
    }

    void InitTileWater(GridTile t)
    {
        if(t.Dpos<=terrainSetting.waterLevel && t.gridTileManager.tileTerrainType == 0)
        {
            t.gridTileManager.tileTerrainType = 2;
            t.gridTileManager.environmentManager.listEnvironment.Add(Environment.listEnvironmentGlobal.Find((x) => x.name=="water"));
        }
    }

    void SetColorTile(GridTile t)
    {
        t.tileGameObject.GetComponent<GridTileGameObject>().SetLineColor(t.gridTileManager.GetColorTile());
    }

    void DrawGameObjectTerrain(GridTile t)
    {
        t.tileGameObject.GetComponent<GridTileGameObject>().DrawGameObject();
    }
}
