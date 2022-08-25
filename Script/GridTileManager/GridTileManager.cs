using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileManager
{
    // Manager
    public BuildingManager buildingManager;
    public EnvironmentManager environmentManager;

    // public var
    // isDestructible is fasle for the first row of tile (to stop the player from reaching the 'core' of the planet)
    public bool isDestructible = true;

    public GridTileManager()
    {
        this.buildingManager = new BuildingManager();
        this.environmentManager = new EnvironmentManager();
    }

    /// <summary>
    /// set Tile Manager
    /// </summary>
    public void SetTileManager(bool destructibility)
    {
        this.isDestructible = destructibility;
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
