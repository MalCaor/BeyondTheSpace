using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlanetGenerationSettings : ScriptableObject
{
    public bool PointTest = false;
    // size of the planet
    public float radius = 1;
    // resolution
    [Range(2, 100)]
    public int resolution = 10;
    // height of the "croute terrestre"
    [Range(1, 10)]
    public int height = 1;
    // draw or not face
    public bool faceDown = true;
    public bool faceBack = true;
    public bool faceUp = true;
    public bool faceForward = true;
    public bool faceLeft = true;
    public bool faceRight = true;
}
