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
    // weight
    bool weightedRandomPixel;

    public Texture2D run(Texture2D InputTexture, int newHeight, int newWidth, bool sansEchec, bool setBorderToFirstPixel, bool weightedRandomPixel)
    {
        // init
        listAllColor = new List<Color>();
        linkNumProxyColbyCol = new Dictionary<Color, Dictionary<Color,int>>();
        int height = InputTexture.height;
        int width = InputTexture.width;
        this.weightedRandomPixel = weightedRandomPixel;

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
                finalMatrix[x, y] = new List<Color>(listAllColor);
            }
        }

        // if setBorderToFirstPixel == true set all border pixel to the 0,0 og pixel
        if(setBorderToFirstPixel)
        {
            for (int x = 0; x < newWidth; x++)
            {
                selectColor(finalMatrix[x, 0], InputTexture.GetPixel(0,0));
                selectColor(finalMatrix[x, newHeight-1], InputTexture.GetPixel(0,0));
            }
            for (int y = 0; y < newHeight; y++)
            {
                selectColor(finalMatrix[0, y], InputTexture.GetPixel(0,0));
                selectColor(finalMatrix[newHeight-1, y], InputTexture.GetPixel(0,0));
            }
        }

        // just set a random col (don't mater)
        int col = 0;
        bool loop = true;
        while (loop)
        {
            //Debug.Log("### PROPAGATE ###");
            propagate(finalMatrix);
            //Debug.Log("### chooseColor ###");
            col = chooseColor(finalMatrix);
            if(col == 0 && !sansEchec)
            {
                // a pixel could not be set
                //Debug.LogError("Erreur");
                loop = false;
            }
            if(col == 2)
            {
                // finish
                //Debug.Log("white found");
                loop = false;
            }
        }
        if(col == 0)
        {
            // finished on a Error
            Debug.Log("Error");
            return null;
        } else
        {
            // set col
            //OutputTexture = new Texture2D(newWidth, newHeight);
            Texture2D text = new Texture2D(newWidth, newHeight);
            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    text.SetPixel(x, y, finalMatrix[x,y][0]);
                }
            }
            Debug.Log("Finish");
            return text;
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

        //Debug.Log("xL : " + xL);
        //Debug.Log("yL : " + yL);

        for (int x = 0; x < xL; x++)
        { 
            for (int y = 0; y < yL; y++)
            {
                int numColor = list[x, y].Count;
                if(numColor == 0)
                {
                    // unsolvable but not us to check
                    //Debug.Log("unsolvable found");
                    continue;
                }
                if(numColor == 1)
                {
                    // Color already selected
                    //Debug.Log("Color Selected");
                    continue;
                }
                // check proxy and update list
                if(x-1>0)
                {
                    if(list[x-1, y].Count==1)
                    {
                        //Debug.Log("found solution");
                        List<Color> colPosible = new List<Color>();
                        Color c = list[x-1, y][0];
                        foreach (Color clink in linkNumProxyColbyCol[c].Keys)
                        {
                            if(linkNumProxyColbyCol[c][clink] > 0 && !colPosible.Contains(clink))
                            {
                                colPosible.Add(clink);
                            }
                        }
                        // list modif is because you can't foreach and modify the list at the same time
                        List<Color> listModif = new List<Color>();
                        foreach (Color cParcour in list[x, y])
                        {
                            if(!colPosible.Contains(cParcour))
                            {
                                listModif.Add(cParcour);
                            }
                        }
                        // remove all colors in list modif
                        foreach (Color cToDelet in listModif)
                        {
                            list[x, y].Remove(cToDelet);
                            //Debug.Log("Remove Color : " + c);
                        }
                    }
                }
                if(x+1>xL)
                {
                    if(list[x+1, y].Count==1)
                    {
                        //Debug.Log("found solution");
                        List<Color> colPosible = new List<Color>();
                        Color c = list[x+1, y][0];
                        foreach (Color clink in linkNumProxyColbyCol[c].Keys)
                        {
                            if(linkNumProxyColbyCol[c][clink] > 0 && !colPosible.Contains(clink))
                            {
                                colPosible.Add(clink);
                            }
                        }
                        // list modif is because you can't foreach and modify the list at the same time
                        List<Color> listModif = new List<Color>();
                        foreach (Color cParcour in list[x, y])
                        {
                            if(!colPosible.Contains(cParcour))
                            {
                                listModif.Add(cParcour);
                            }
                        }
                        // remove all colors in list modif
                        foreach (Color cToDelet in listModif)
                        {
                            list[x, y].Remove(cToDelet);
                            //Debug.Log("Remove Color : " + c);
                        }
                    }
                }
                if(y-1>0)
                {
                    if(list[x, y-1].Count==1)
                    {
                        //Debug.Log("found solution");
                        List<Color> colPosible = new List<Color>();
                        Color c = list[x, y-1][0];
                        foreach (Color clink in linkNumProxyColbyCol[c].Keys)
                        {
                            if(linkNumProxyColbyCol[c][clink] > 0 && !colPosible.Contains(clink))
                            {
                                colPosible.Add(clink);
                            }
                        }
                        // list modif is because you can't foreach and modify the list at the same time
                        List<Color> listModif = new List<Color>();
                        foreach (Color cParcour in list[x, y])
                        {
                            if(!colPosible.Contains(cParcour))
                            {
                                listModif.Add(cParcour);
                            }
                        }
                        // remove all colors in list modif
                        foreach (Color cToDelet in listModif)
                        {
                            list[x, y].Remove(cToDelet);
                            //Debug.Log("Remove Color : " + c);
                        }
                    }
                }
                if(y+1<yL)
                {
                    if(list[x, y+1].Count==1)
                    {
                        //Debug.Log("found solution");
                        List<Color> colPosible = new List<Color>();
                        Color c = list[x, y+1][0];
                        foreach (Color clink in linkNumProxyColbyCol[c].Keys)
                        {
                            if(linkNumProxyColbyCol[c][clink] > 0 && !colPosible.Contains(clink))
                            {
                                colPosible.Add(clink);
                            }
                        }
                        // list modif is because you can't foreach and modify the list at the same time
                        List<Color> listModif = new List<Color>();
                        foreach (Color cParcour in list[x, y])
                        {
                            if(!colPosible.Contains(cParcour))
                            {
                                listModif.Add(cParcour);
                            }
                        }
                        // remove all colors in list modif
                        foreach (Color cToDelet in listModif)
                        {
                            list[x, y].Remove(cToDelet);
                            //Debug.Log("Remove Color : " + c);
                        }
                    }
                }
            }
        }
    }

    // return int (1 = ok, 2 = finish, 0 = black)
    int chooseColor(List<Color>[,] list)
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
                //Debug.Log("Pixel : " + x + ", " + y + " count : " + list[x, y].Count);
                // check id count is bellow countTarget AND is it's not already selected (=1)
                if(list[x, y].Count < countTarget && list[x, y].Count != 1)
                {
                    //Debug.Log("found a pixel with les entropy : " + list[x, y].Count);
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
            //Debug.Log("Black");
            return 0;
        }

        if(countTarget == int.MaxValue)
        {
            // every color have been selected
            //Debug.Log("finish");
            return 2;
        }

        //Debug.Log("Normal");
        // TODO : code wheighted random
        if(weightedRandomPixel)
        {
            List<Color> listColorRandom = new List<Color>();
            foreach (Color cloParcour in list[xTarget, yTarget])
            {
                if(xTarget != 0)
                {
                    if(list[xTarget-1, yTarget].Count == 1)
                    {
                        for (int i = 0; i < linkNumProxyColbyCol[list[xTarget-1, yTarget][0]][cloParcour]; i++)
                        {
                            listColorRandom.Add(cloParcour);
                        }
                    }
                }
                if(xTarget != xL)
                {
                    if(list[xTarget+1, yTarget].Count == 1)
                    {
                        for (int i = 0; i < linkNumProxyColbyCol[list[xTarget+1, yTarget][0]][cloParcour]; i++)
                        {
                            listColorRandom.Add(cloParcour);
                        }
                        
                    }
                }
                if(yTarget != 0)
                {
                    if(list[xTarget, yTarget-1].Count == 1)
                    {
                        for (int i = 0; i < linkNumProxyColbyCol[list[xTarget, yTarget-1][0]][cloParcour]; i++)
                        {
                            listColorRandom.Add(cloParcour);
                        }
                        
                    }
                }
                if(yTarget != yL)
                {
                    if(list[xTarget, yTarget+1].Count == 1)
                    {
                        for (int i = 0; i < linkNumProxyColbyCol[list[xTarget, yTarget+1][0]][cloParcour]; i++)
                        {
                            listColorRandom.Add(cloParcour);
                        }
                        
                    }
                }
                listColorRandom.Add(cloParcour);
            }
            // select a random num
            int r = Mathf.RoundToInt(Random.Range(0, listColorRandom.Count));
            // select the color
            selectColor(list[xTarget, yTarget], listColorRandom[r]);
            return 1;
        } else {
            // select a random num
            int r = Mathf.RoundToInt(Random.Range(0, countTarget));
            // select the color
            selectColor(list[xTarget, yTarget], list[xTarget, yTarget][r]);
            return 1;
        }
        
    }
}
