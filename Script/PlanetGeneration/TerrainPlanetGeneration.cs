using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainPlanetGeneration : MonoBehaviour
{
    // grid
    GridPlanetGeneration grid;

    // noise
    Noise noise = new Noise();

    // setting
    public TerrainPlanetGenerationSetting terrainSetting;

    public void Init()
    {
        grid = gameObject.GetComponent<GridPlanetGeneration>();
        // init all env
        EnvironmentGlobalGeneration.InitAllEnvironment();
        InitTerrain();
        ResetColor();
        if (terrainSetting.water)
        {
            InitWater();
        }
        SetColor();
        ShowHideTerrain();
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

    void ShowHideTerrain()
    {
        // Nord
        foreach (GridTile tile in grid.FaceNord)
        {
            ShowHideTileTerrain(tile);
        }
        // Est
        foreach (GridTile tile in grid.FaceEst)
        {
            ShowHideTileTerrain(tile);
        }
        // Ouest
        foreach (GridTile tile in grid.FaceOuest)
        {
            ShowHideTileTerrain(tile);
        }
        // Front
        foreach (GridTile tile in grid.FaceFront)
        {
            ShowHideTileTerrain(tile);
        }
        // Back
        foreach (GridTile tile in grid.FaceBack)
        {
            ShowHideTileTerrain(tile);
        }
        // Sud
        foreach (GridTile tile in grid.FaceSud)
        {
            ShowHideTileTerrain(tile);
        }
    }

    void ResetColorTile(GridTile t)
    {
        t.tileGameObject.GetComponent<GridTileGameObject>().SetLineColor(Color.black);
    }

    void InitTileTerrain(GridTile t)
    {
        // generate terrain
        float noiseVal = (noise.Evaluate(new Vector2(t.Npos, t.Opos) * terrainSetting.roughnessNoiseTerrainElevation)+1) * 0.5f;
        int levelTerrain = (int)Mathf.Round(grid.planetSettings.height * noiseVal);
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

    void ShowHideTileTerrain(GridTile t)
    {
        if(t.gridTileManager.tileTerrainType == 0)
        {
            t.tileGameObject.GetComponent<GridTileGameObject>().HideTileLine();
        } else {
            t.tileGameObject.GetComponent<GridTileGameObject>().ShowTileLine();
            t.tileGameObject.GetComponent<GridTileGameObject>().UpdatePointMesh();
            t.tileGameObject.GetComponent<GridTileGameObject>().drawMesh();
        }
        try{
            if(t.ProxyTileNord.gridTileManager.tileTerrainType == 1 &&
            t.ProxyTileSud.gridTileManager.tileTerrainType == 1 &&
            t.ProxyTileOuest.gridTileManager.tileTerrainType == 1 &&
            t.ProxyTileEst.gridTileManager.tileTerrainType == 1 &&
            t.ProxyTileUp.gridTileManager.tileTerrainType == 1)
            {
                t.tileGameObject.GetComponent<GridTileGameObject>().HideTileLine();
            }
        }
        catch
        {

        }
    }
}
