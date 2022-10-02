using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WFCText2DSettings : ScriptableObject
{
    // Settings for the WFC Texture2D algo

    public Texture2D InputTexture;
    public int newHeight;
    public int newWidth;
    public bool sansEchec;
    public bool setBorderToFirstPixel;
    public bool weightedRandomPixel;
    public bool ogPixelFavoritism;
    public int ogPixelFavoritismIntensity;
    public bool firstFewIterationTrueRandom;
    public int numberIterationTrueRandom;
}
