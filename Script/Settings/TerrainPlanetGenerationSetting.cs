using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class TerrainPlanetGenerationSetting : ScriptableObject
{
    // seting for terrain generation
    public bool water = true;
    public int waterLevel;
    public int SeedTerrainGeneration = 1;
    // rougness of the noise
    public float roughnessNoiseTerrainElevation = 1f;
}
