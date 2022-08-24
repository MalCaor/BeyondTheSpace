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
}
