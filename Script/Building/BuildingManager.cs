using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager
{
    public List<Building> listBuilding;

    public BuildingManager()
    {
        listBuilding = new List<Building>();
    }

    // SetColorTile for Debug puposes
    public Color GetColorTile()
    {
        Color c = Color.black;
        return c;
    }
}
