using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class WFCcontroler : MonoBehaviour
{
    // vars
    public Texture2D InputTexture;
    public string OutputTexture;
    public int newHeight;
    public int newWidth;
    public bool sansEchec;
    public bool setBorderToFirstPixel;
    public bool weightedRandomPixel;
    public bool ogPixelFavoritism;

    public void Init()
    {
        WaveFunctionCollapseTexture2D WFC = new WaveFunctionCollapseTexture2D();
        Texture2D text = WFC.run(InputTexture, newHeight, newWidth, sansEchec, setBorderToFirstPixel, weightedRandomPixel, ogPixelFavoritism);
        // save texture
        byte[] pngBytes = text.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + OutputTexture, pngBytes);
    }
}
