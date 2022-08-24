using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileManager
{
    public BuildingManager buildingManager;
    public EnvironmentManager environmentManager;

    public GridTileManager()
    {
        this.buildingManager = new BuildingManager();
        this.environmentManager = new EnvironmentManager();
    }

    // SetColorTile for Debug puposes
    public Color GetColorTile()
    {
        Color c = Color.black;
        c = buildingManager.GetColorTile();
        if(c==Color.black)
        {
            // black is default
            c = environmentManager.GetColorTile();
        }
        return c;
    }
}
