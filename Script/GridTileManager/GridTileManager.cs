using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridTileManager
{
    // Manager
    public BuildingManager buildingManager;
    public EnvironmentManager environmentManager;

    // public var
    // isDestructible is fasle for the first row of tile (to stop the player from reaching the 'core' of the planet)
    public bool isDestructible = true;
    // type tile (0 = Air / 1 = Solid / 2 = Liquid / 3 = Space)
    public int tileTerrainType = 0;

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

    public Material GetMaterialTile()
    {
        if(tileTerrainType == 1)
        {
            // Solid
            return Resources.Load("Material/BlackMatDef") as Material;
        } else {
            // Liquid
            return Resources.Load("Material/BlueMatDef") as Material;
        }
        
    }
}
