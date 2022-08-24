using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager
{
    public List<Environment> listEnvironment;

    public EnvironmentManager()
    {
        listEnvironment = new List<Environment>();
    }

    // SetColorTile for Debug puposes
    public Color GetColorTile()
    {
        Color c = Color.black;
        return c;
    }
}
