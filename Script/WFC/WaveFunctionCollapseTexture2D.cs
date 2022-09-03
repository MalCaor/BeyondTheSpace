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

    public void run(Texture2D InputTexture, Texture2D OutputTexture, int newHeight, int newWidth, bool sansEchec)
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

        // fill final mat with all col possible
        finalMatrix = new List<Color>[newWidth, newHeight];
        for (int x = 0; x < newWidth; x++)
        {
            for (int y = 0; y < newHeight; y++)
            {
                // set all color as posibility
                finalMatrix[x, y] = listAllColor;
            }
        }

        // just set a random col (don't mater)
        Color col = Color.black;
        bool loop = true;
        while (loop)
        {
            Debug.Log("### PROPAGATE ###");
            propagate(finalMatrix);
            Debug.Log("### chooseColor ###");
            col = chooseColor(finalMatrix);
            if(col == Color.black && !sansEchec)
            {
                // a pixel could not be set
                Debug.LogError("Erreur");
                loop = false;
            }
            if(col == Color.white)
            {
                // finish
                Debug.Log("white found");
                loop = false;
            }
        }
        if(col == Color.black)
        {
            // finished on a Error
        } else
        {
            // set col
            OutputTexture = new Texture2D(newWidth, newHeight);
            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    //Debug.Log("OG Pix : " + OutputTexture.GetPixel(x,y));
                    //Debug.Log("NEW Pix : " + finalMatrix[x,y][0]);
                    OutputTexture.SetPixel(x, y, finalMatrix[x,y][0]);
                }
            }
            OutputTexture.Apply();
            Debug.Log("Finish");
        }
    }

    void selectColor(List<Color> list, Color c)
    {
        // let only the selected color int the list
        list.Clear();
        list.Add(c);
    }

    void propagate(List<Color>[,] list)
    {
        int xL = list.GetLength(0);
        int yL = list.GetLength(1);

        Debug.Log("xL : " + xL);
        Debug.Log("yL : " + yL);

        for (int x = 0; x < xL; x++)
        {
            for (int y = 0; y < yL; y++)
            {
                int numColor = list[x, y].Count;
                if(numColor == 0)
                {
                    // unsolvable but not us to check
                    Debug.Log("unsolvable found");
                    continue;
                }
                if(numColor == 1)
                {
                    // Color already selected
                    Debug.Log("Color Selected");
                    continue;
                }
                // check proxy and update list
                if(x-1>0)
                {
                    List<Color> colPosible = new List<Color>();
                    foreach (Color c in list[x-1, y])
                    {
                        foreach (Color clink in linkNumProxyColbyCol[c].Keys)
                        {
                            if(linkNumProxyColbyCol[c][clink] > 0 && !colPosible.Contains(clink))
                            {
                                colPosible.Add(clink);
                            }
                        }
                    }
                    // list modif is because you can't foreach and modify the list at the same time
                    List<Color> listModif = new List<Color>();
                    foreach (Color c in list[x, y])
                    {
                        if(!colPosible.Contains(c))
                        {
                            listModif.Add(c);
                        }
                    }
                    // remove all colors in list modif
                    foreach (Color c in listModif)
                    {
                        list[x, y].Remove(c);
                        Debug.Log("Remove Color : " + c);
                    }
                }
                if(x+1>xL)
                {
                    List<Color> colPosible = new List<Color>();
                    foreach (Color c in list[x+1, y])
                    {
                        foreach (Color clink in linkNumProxyColbyCol[c].Keys)
                        {
                            if(linkNumProxyColbyCol[c][clink] > 0 && !colPosible.Contains(clink))
                            {
                                colPosible.Add(clink);
                            }
                        }
                    }
                    // list modif is because you can't foreach and modify the list at the same time
                    List<Color> listModif = new List<Color>();
                    foreach (Color c in list[x, y])
                    {
                        if(!colPosible.Contains(c))
                        {
                            listModif.Add(c);
                        }
                    }
                    // remove all colors in list modif
                    foreach (Color c in listModif)
                    {
                        list[x, y].Remove(c);
                        Debug.Log("Remove Color : " + c);
                    }
                }
                if(y-1>0)
                {
                    List<Color> colPosible = new List<Color>();
                    foreach (Color c in list[x, y-1])
                    {
                        foreach (Color clink in linkNumProxyColbyCol[c].Keys)
                        {
                            if(linkNumProxyColbyCol[c][clink] > 0 && !colPosible.Contains(clink))
                            {
                                colPosible.Add(clink);
                            }
                        }
                    }
                    // list modif is because you can't foreach and modify the list at the same time
                    List<Color> listModif = new List<Color>();
                    foreach (Color c in list[x, y])
                    {
                        if(!colPosible.Contains(c))
                        {
                            listModif.Add(c);
                        }
                    }
                    // remove all colors in list modif
                    foreach (Color c in listModif)
                    {
                        list[x, y].Remove(c);
                        Debug.Log("Remove Color : " + c);
                    }
                }
                if(y+1<yL)
                {
                    List<Color> colPosible = new List<Color>();
                    foreach (Color c in list[x, y+1])
                    {
                        foreach (Color clink in linkNumProxyColbyCol[c].Keys)
                        {
                            if(linkNumProxyColbyCol[c][clink] > 0 && !colPosible.Contains(clink))
                            {
                                colPosible.Add(clink);
                            }
                        }
                    }
                    // list modif is because you can't foreach and modify the list at the same time
                    List<Color> listModif = new List<Color>();
                    foreach (Color c in list[x, y])
                    {
                        if(!colPosible.Contains(c))
                        {
                            listModif.Add(c);
                        }
                    }
                    // remove all colors in list modif
                    foreach (Color c in listModif)
                    {
                        list[x, y].Remove(c);
                        Debug.Log("Remove Color : " + c);
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

        int xTarget = 0;
        int yTarget = 0;
        int countTarget = int.MaxValue;

        // calculate entropy (lowest number of colors posible)
        for (int x = 0; x < xL; x++)
        {
            for (int y = 0; y < yL; y++)
            {
                // check id count is bellow countTarget AND is it's not already selected (=1)
                if(list[x, y].Count < countTarget && list[x, y].Count != 1)
                {
                    countTarget = list[x, y].Count;
                    xTarget = x;
                    yTarget = y;
                }
            }
        }
        
        // test if 0
        if(countTarget == 0)
        {
            // there is a pixel with no Color posible
            selectColor(list[xTarget, yTarget], Color.black);
            Debug.Log("Black");
            return Color.black;
        }

        if(countTarget == int.MaxValue)
        {
            // every color have been selected
            Debug.Log("white");
            return Color.white;
        }

        Debug.Log("Normal");
        // TODO : code wheighted random
        // select a random num
        int r = Mathf.RoundToInt(Random.Range(0, countTarget));
        // select the color
        selectColor(list[xTarget, yTarget], list[xTarget, yTarget][r]);
        return list[xTarget, yTarget][0];
    }
}
