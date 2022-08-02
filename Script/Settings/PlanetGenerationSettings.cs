using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlanetGenerationSettings : ScriptableObject
{
    public float radius = 1;
    // resolution
    [Range(2, 100)]
    public int resolution = 10;
    // height of the "croute terrestre"
    [Range(1, 10)]
    public int height = 1;
}
