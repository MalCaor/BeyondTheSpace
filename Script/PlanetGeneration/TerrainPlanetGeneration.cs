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

    void InitTileTerrain(GridTile t)
    {
        float noiseVal = (noise.Evaluate(new Vector2(t.Npos, t.Opos))+1) * 0.5f;
        Debug.Log(noiseVal);
        t.tileGameObject.GetComponent<GridTileGameObject>().HideTileLine();
    }
}
