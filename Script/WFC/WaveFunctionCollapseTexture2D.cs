using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapseTexture2D
{
    // vars
    // all color in the og img
    List<Color> listAllColor;
    // all proxy of the colors
    Dictionary<Color, List<Dictionary<Color,int>>> linkNumProxyColbyCol;

    public void run(Texture2D InputTexture)
    {
        // init
        listAllColor = new List<Color>();
        linkNumProxyColbyCol = new Dictionary<Color, List<Dictionary<Color,int>>>();
        int height = InputTexture.height;
        int width = InputTexture.width;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Color curPixel = InputTexture.GetPixel(x, y);
                // add to simple list
                if(!listAllColor.Contains(curPixel))
                {
                    listAllColor.Add(curPixel);
                }
                // add to dic
                if(!linkNumProxyColbyCol.ContainsKey(curPixel))
                {
                    linkNumProxyColbyCol.Add(curPixel, new List<Dictionary<Color,int>>());
                }
            }
        }
    }
}
