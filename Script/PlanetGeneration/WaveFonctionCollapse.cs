using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFonctionCollapse
{
    ///
    /// Class using the wave fonction collapse algo to get a BitMap output from a BitMap input
    /// will be used to create the terrain of a face
    ///

    public void GetAllColor(Texture2D ogBitMap)
    {
        for (int h = 0; h < ogBitMap.height; h++)
        {
            for (int w = 0; w < ogBitMap.width; w++)
            {
                Debug.Log(ogBitMap.GetPixel(h,w));
            }
        }
    }
}
