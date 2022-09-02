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
        // let only the selected color int the list
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
                    // unsolvable but not us to check
                    continue;
                }
                if(numColor == 1)
                {
                    // Color already selected
                    continue;
                }
                // check proxy and update list
                if(x-1>0)
                {
                    List<Color> colPosible = new List<Color>();
                    foreach (Color c in list[x-1, y])
                    {
                        colPosible.AddRange(linkNumProxyColbyCol[c].Keys);
                    }
                    foreach (Color c in list[x, y])
                    {
                        if(!colPosible.Contains(c))
                        {
                            list[x, y].Remove(c);
                        }
                    }
                }
                if(x+1>xL)
                {
                    List<Color> colPosible = new List<Color>();
                    foreach (Color c in list[x+1, y])
                    {
                        colPosible.AddRange(linkNumProxyColbyCol[c].Keys);
                    }
                    foreach (Color c in list[x, y])
                    {
                        if(!colPosible.Contains(c))
                        {
                            list[x, y].Remove(c);
                        }
                    }
                }
                if(y-1>0)
                {
                    List<Color> colPosible = new List<Color>();
                    foreach (Color c in list[x, y-1])
                    {
                        colPosible.AddRange(linkNumProxyColbyCol[c].Keys);
                    }
                    foreach (Color c in list[x, y])
                    {
                        if(!colPosible.Contains(c))
                        {
                            list[x, y].Remove(c);
                        }
                    }
                }
                if(y+1<yL)
                {
                    List<Color> colPosible = new List<Color>();
                    foreach (Color c in list[x, y+1])
                    {
                        colPosible.AddRange(linkNumProxyColbyCol[c].Keys);
                    }
                    foreach (Color c in list[x, y])
                    {
                        if(!colPosible.Contains(c))
                        {
                            list[x, y].Remove(c);
                        }
                    }
                }
            }
        }
    }

    Color chooseColor(List<Color>[,] list)
    {
        // choose a random color form the pixel with the lowest entropy
        int xL = list.GetLength(0);
        int yL = list.GetLength(1);

        int xTarget;
        int yTarget;
        int countTarget = int.MaxValue;

        for (int x = 0; x < xL; x++)
        {
            for (int y = 0; y < yL; y++)
            {
                if(list[x, y].Count < countTarget)
                {
                    countTarget = list[x, y].Count;
                    xTarget = x;
                    yTarget = y;
                }
            }
        }
        // TODO : finish
        return Color.black;
    }
}
