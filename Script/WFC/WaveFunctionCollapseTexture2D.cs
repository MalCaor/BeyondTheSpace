using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapseTexture2D
{
    // vars
    // all color in the og img
    List<Color> listAllColor;
    // all proxy of the colors
    Dictionary<Color, Dictionary<Color,int>> linkNumProxyColbyCol;
    // final pixel matrix
    List<Color>[,] finalMatrix;

    public void run(Texture2D InputTexture, Texture2D OutputTexture, int newHeight, int newWidth)
    {
        // init
        listAllColor = new List<Color>();
        linkNumProxyColbyCol = new Dictionary<Color, Dictionary<Color,int>>();
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
                    linkNumProxyColbyCol.Add(curPixel, new Dictionary<Color,int>());
                }

                // Increment proxy
                if(x-1>0)
                {
                    Color p = InputTexture.GetPixel(x-1, y);
                    if(!linkNumProxyColbyCol[curPixel].ContainsKey(p))
                    {
                        linkNumProxyColbyCol[curPixel].Add(p, 0);
                    }
                    linkNumProxyColbyCol[curPixel][p] ++;
                }
                if(x+1>width)
                {
                    Color p = InputTexture.GetPixel(x+1, y);
                    if(!linkNumProxyColbyCol[curPixel].ContainsKey(p))
                    {
                        linkNumProxyColbyCol[curPixel].Add(p, 0);
                    }
                    linkNumProxyColbyCol[curPixel][p] ++;
                }
                if(y-1>0)
                {
                    Color p = InputTexture.GetPixel(x, y-1);
                    if(!linkNumProxyColbyCol[curPixel].ContainsKey(p))
                    {
                        linkNumProxyColbyCol[curPixel].Add(p, 0);
                    }
                    linkNumProxyColbyCol[curPixel][p] ++;
                }
                if(y+1<height)
                {
                    Color p = InputTexture.GetPixel(x, y+1);
                    if(!linkNumProxyColbyCol[curPixel].ContainsKey(p))
                    {
                        linkNumProxyColbyCol[curPixel].Add(p, 0);
                    }
                    linkNumProxyColbyCol[curPixel][p] ++;
                }
            }
        }

        // set missing color
        foreach (Color c in listAllColor)
        {
            foreach (Color c2 in listAllColor)
            {
                if(!linkNumProxyColbyCol[c].ContainsKey(c2))
                {
                    linkNumProxyColbyCol[c].Add(c2, 0);
                }
            }
        }

        finalMatrix = new List<Color>[newWidth, newHeight];
        for (int x = 0; x < newWidth; x++)
        {
            for (int y = 0; y < newHeight; y++)
            {
                // set all color as posibility
                finalMatrix[x, y] = listAllColor;
            }
        }
    }

    void selectColor(List<Color> list, Color c)
    {
        foreach (Color cl in list)
        {
            if(!cl.Equals(c))
            {
                list.Remove(cl);
            }
        }
    }

    void propagate(List<Color>[,] list)
    {
        int xL = list.GetLength(0);
        int yL = list.GetLength(1);

        for (int x = 0; x < xL; x++)
        {
            for (int y = 0; y < yL; y++)
            {
                int numColor = list[x, y].Count;
                if(numColor == 0)
                {
                    Debug.LogError("Cas insolvable");
                }
                if(numColor == 1)
                {
                    // Color already selected
                    continue;
                }
                // check proxy and update list
                if(x-1>0)
                {
                    if(list[x-1,y].Count == 1)
                    {
                        // there is a pixel set
                        foreach (Color c in list[x,y])
                        {
                            // get c color posible
                            if(!linkNumProxyColbyCol[list[x-1,y][0]].ContainsKey(c))
                            {
                                // remove if 0 proba
                                list[x,y].Remove(c);
                            }
                        }
                    }
                }
                if(x+1>xL)
                {
                    if(list[x+1,y].Count == 1)
                    {
                        // there is a pixel set
                        foreach (Color c in list[x,y])
                        {
                            // get c color posible
                            if(!linkNumProxyColbyCol[list[x+1,y][0]].ContainsKey(c))
                            {
                                // remove if 0 proba
                                list[x,y].Remove(c);
                            }
                        }
                    }
                }
                if(y-1>0)
                {
                    if(list[x,y-1].Count == 1)
                    {
                        // there is a pixel set
                        foreach (Color c in list[x,y])
                        {
                            // get c color posible
                            if(!linkNumProxyColbyCol[list[x,y-1][0]].ContainsKey(c))
                            {
                                // remove if 0 proba
                                list[x,y].Remove(c);
                            }
                        }
                    }
                }
                if(y+1<yL)
                {
                    if(list[x,y+1].Count == 1)
                    {
                        // there is a pixel set
                        foreach (Color c in list[x,y])
                        {
                            // get c color posible
                            if(!linkNumProxyColbyCol[list[x,y+1][0]].ContainsKey(c))
                            {
                                // remove if 0 proba
                                list[x,y].Remove(c);
                            }
                        }
                    }
                }
            }
        }
    }
}
