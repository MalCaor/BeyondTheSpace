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
        InitTerrain();
        InitWater();
        ColorWaterAndSand();
        ShowHideTerrain();
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

    void ColorWaterAndSand()
    {
        // Nord
        foreach (GridTile tile in grid.FaceNord)
        {
            ColorTileWaterAndSand(tile);
        }
        // Est
        foreach (GridTile tile in grid.FaceEst)
        {
            ColorTileWaterAndSand(tile);
        }
        // Ouest
        foreach (GridTile tile in grid.FaceOuest)
        {
            ColorTileWaterAndSand(tile);
        }
        // Front
        foreach (GridTile tile in grid.FaceFront)
        {
            ColorTileWaterAndSand(tile);
        }
        // Back
        foreach (GridTile tile in grid.FaceBack)
        {
            ColorTileWaterAndSand(tile);
        }
        // Sud
        foreach (GridTile tile in grid.FaceSud)
        {
            ColorTileWaterAndSand(tile);
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

    void InitTileTerrain(GridTile t)
    {
        // generate terrain
        float noiseVal = (noise.Evaluate(new Vector2(t.Npos, t.Opos) * terrainSetting.roughnessNoiseTerrainElevation)+1) * 0.5f;
        int levelTerrain = (int)Mathf.Round(grid.planetSettings.height * noiseVal);
        if(t.Dpos>levelTerrain)
        {
            t.gridTileManager.tileTerrainType = 0;
            t.tileGameObject.GetComponent<GridTileGameObject>().HideTileLine();
        } else {
            t.gridTileManager.tileTerrainType = 1;
            t.tileGameObject.GetComponent<GridTileGameObject>().ShowTileLine();
        }
    }

    void InitTileWater(GridTile t)
    {
        if(t.Dpos<=terrainSetting.waterLevel && t.gridTileManager.tileTerrainType == 0)
        {
            t.gridTileManager.tileTerrainType = 2;
            t.tileGameObject.GetComponent<GridTileGameObject>().ShowTileLine();
            t.tileGameObject.GetComponent<GridTileGameObject>().SetLineColor(Color.blue);
        }
    }

    void ColorTileWaterAndSand(GridTile t)
    {
        try{
            if(t.gridTileManager.tileTerrainType == 1 &&(
                t.ProxyTileNord.gridTileManager.tileTerrainType == 2 ||
                t.ProxyTileSud.gridTileManager.tileTerrainType == 2 ||
                t.ProxyTileOuest.gridTileManager.tileTerrainType == 2 ||
                t.ProxyTileEst.gridTileManager.tileTerrainType == 2 ||
                t.ProxyTileUp.gridTileManager.tileTerrainType == 2))
            {
                t.tileGameObject.GetComponent<GridTileGameObject>().SetLineColor(Color.yellow);
            }
        }
        catch
        {

        }
    }

    void ShowHideTileTerrain(GridTile t)
    {
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
