using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BiomBeyondTheSpace;
using BuildingBeyondTheSpace;
using SettingsBeyondTheSpace;
using GridTileBeyondTheSpace;
using EnvironmentBeyondTheSpace;

public class BiomTestClass : Biom, BiomInterface
{
    public BiomTestClass(Color color, string name) : base(color, name)
    {

    }

    public List<Environment> GetEnvironments(GridTile gridTile, TerrainPlanetGenerationSetting terrainSetting)
    {
        return null;
    }

    public List<Building> GetBuildings(GridTile gridTile, TerrainPlanetGenerationSetting terrainSetting)
    {
        return null;
    }
}
