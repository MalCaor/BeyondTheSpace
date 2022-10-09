using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// custom Class Using
using SettingsBeyondTheSpace;

namespace WFCBeyondTheSpace
{
    /// <Summary> 
    /// Generate a Texture2D from an Texture2D Input </br>
    /// while try to find rules in the of Image and create a new one from it
    /// </Summary>
    public class WaveFunctionCollapseTexture2D
    {
        // vars
        /// <summary>all color in the og img</summary>
        List<Color> listAllColor;
        /// <summary>all proxy of the colors</summary>
        Dictionary<Color, Dictionary<Color,int>> linkNumProxyColbyCol;
        /// <summary>final pixel matrix</summary>
        List<Color>[,] finalMatrix;
        /// <summary>WFC Settings</summary>
        WFCText2DSettings settings;
        /// <summary>number of Iteration</summary>
        int numIteration;

        /// <summary>
        /// run the wfc algo and return a Texture2D
        /// </summary>
        /// <param name="settings">Settings (params) of the generation.</param>
        public Texture2D run(WFCText2DSettings settings)
        {
            // set vars used by the entire prog
            setInputArg(settings);

            // get the list of proba from og image
            fillListLinkColor();

            // fill the entire matrice with all possible colors
            fillMatWithAllColor();

            // if true every border is of the 0,0 pixel (generally water for continuity)
            if(settings.setBorderToFirstPixel)
            {
                this.setBorderToFirstPixel();
            }

            // col is the return of chooseColor (0 = error, 1 = ok, 2 = finished (no more pixel to set))
            int col = 0;
            bool loop = true;
            // loop until all pixel are selected
            while (loop)
            {
                // spread proba
                propagate(finalMatrix);
                // choose a color
                col = chooseColor(finalMatrix);
                // interprete the col output
                if(col == 2)
                {
                    // finish
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
                // set return texture
                Texture2D text = new Texture2D(settings.newWidth, settings.newHeight);
                for (int x = 0; x < settings.newWidth; x++)
                {
                    for (int y = 0; y < settings.newHeight; y++)
                    {
                        text.SetPixel(x, y, finalMatrix[x,y][0]);
                    }
                }
                Debug.Log("Finish");
                return text;
            }
        }

        /// <summary>
        /// set Class vars with input args
        /// </summary>
        /// <param name="settings">Settings (params) of the generation.</param>
        private void setInputArg(WFCText2DSettings settings)
        {
            // init class var with arg
            this.numIteration = 0;
            listAllColor = new List<Color>();
            linkNumProxyColbyCol = new Dictionary<Color, Dictionary<Color,int>>();
            this.settings = settings;
        }

        /// <summary>
        /// fill list color posible and their proxy proba
        /// </summary>
        private void fillListLinkColor()
        {
            for (int x = 0; x < settings.InputTexture.width; x++)
            {
                for (int y = 0; y < settings.InputTexture.height; y++)
                {
                    Color curPixel = settings.InputTexture.GetPixel(x, y);
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
                        Color p = settings.InputTexture.GetPixel(x-1, y);
                        if(!linkNumProxyColbyCol[curPixel].ContainsKey(p))
                        {
                            linkNumProxyColbyCol[curPixel].Add(p, 0);
                        }
                        linkNumProxyColbyCol[curPixel][p] ++;
                    }
                    if(x+1>settings.InputTexture.width)
                    {
                        Color p = settings.InputTexture.GetPixel(x+1, y);
                        if(!linkNumProxyColbyCol[curPixel].ContainsKey(p))
                        {
                            linkNumProxyColbyCol[curPixel].Add(p, 0);
                        }
                        linkNumProxyColbyCol[curPixel][p] ++;
                    }
                    if(y-1>0)
                    {
                        Color p = settings.InputTexture.GetPixel(x, y-1);
                        if(!linkNumProxyColbyCol[curPixel].ContainsKey(p))
                        {
                            linkNumProxyColbyCol[curPixel].Add(p, 0);
                        }
                        linkNumProxyColbyCol[curPixel][p] ++;
                    }
                    if(y+1<settings.InputTexture.height)
                    {
                        Color p = settings.InputTexture.GetPixel(x, y+1);
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
        }

        /// <summary>
        /// fill final mat with all col possible
        /// </summary>
        private void fillMatWithAllColor()
        {
            // fill final mat with all col possible
            finalMatrix = new List<Color>[settings.newWidth, settings.newHeight];
            for (int x = 0; x < settings.newWidth; x++)
            {
                for (int y = 0; y < settings.newHeight; y++)
                {
                    // set all color as posibility
                    finalMatrix[x, y] = new List<Color>(listAllColor);
                }
            }
        }

        /// <summary>
        /// set all border pixel to the 0,0 og pixel
        /// </summary>
        private void setBorderToFirstPixel()
        {
            for (int x = 0; x < settings.newWidth; x++)
            {
                selectColor(finalMatrix[x, 0], settings.InputTexture.GetPixel(0,0));
                selectColor(finalMatrix[x, settings.newHeight-1], settings.InputTexture.GetPixel(0,0));
            }
            for (int y = 0; y < settings.newHeight; y++)
            {
                selectColor(finalMatrix[0, y], settings.InputTexture.GetPixel(0,0));
                selectColor(finalMatrix[settings.newHeight-1, y], settings.InputTexture.GetPixel(0,0));
            }
        }

        /// <summary>
        /// input list have now only int input color
        /// </summary>
        /// <param name="list">the List representing the pixel selected.</param>
        /// <param name="c">the Color Selected.</param>
        private void selectColor(List<Color> list, Color c)
        {
            // let only the selected color int the list
            list.Clear();
            list.Add(c);
        }

        /// <summary>
        /// propagate the probality from proxy pixel
        /// </summary>
        /// <param name="list">the List representing color matrix.</param>
        private void propagate(List<Color>[,] list)
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
                        // unsolvable so add all color again
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
                        if(list[x-1, y].Count==1 && list[x-1, y][0] != Color.black)
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
                        if(list[x+1, y].Count==1 && list[x+1, y][0] != Color.black)
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
                        if(list[x, y-1].Count==1 && list[x, y-1][0] != Color.black)
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
                        if(list[x, y+1].Count==1 && list[x, y+1][0] != Color.black)
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

        /// <summary>
        /// choose a random color form the pixel with the lowest entropy or random.
        /// return int (1 = ok, 2 = finish, 0 = black)
        /// </summary>
        /// <param name="list">the List representing color matrix.</param>
        private int chooseColor(List<Color>[,] list)
        {
            // choose a random color form the pixel with the lowest entropy
            int xL = list.GetLength(0);
            int yL = list.GetLength(1);

            int xTarget = 0;
            int yTarget = 0;
            int countTarget = int.MaxValue;

            if(settings.firstFewIterationTrueRandom && numIteration<settings.numberIterationTrueRandom)
            {
                xTarget = Random.Range(0, xL-1);
                yTarget = Random.Range(0, yL-1);
                countTarget = list[xTarget, yTarget].Count;
            } else {
                // calculate entropy (lowest number of colors posible) from random list of vector
                List<Vector2> listPosPixel = new List<Vector2>();
                for (int x = 0; x < xL; x++)
                {
                    for (int y = 0; y < yL; y++)
                    {
                        // check id count is bellow countTarget AND is it's not already selected (=1)
                        if(list[x, y].Count < countTarget && list[x, y].Count != 1)
                        {
                            listPosPixel = new List<Vector2>();
                            countTarget = list[x, y].Count;
                            listPosPixel.Add(new Vector2(x,y));
                        } else if(list[x, y].Count == countTarget && list[x, y].Count != 1)
                        {
                            listPosPixel.Add(new Vector2(x,y));
                        }
                    }
                }
                // choose random col if list not empty
                if(listPosPixel.Count!=0)
                {
                    int r = Random.Range(0,listPosPixel.Count);
                    xTarget = (int)listPosPixel[r].x;
                    yTarget = (int)listPosPixel[r].y;
                }
            }
            
            numIteration++;
            
            // test if 0
            if(countTarget == 0)
            {
                // there is a pixel with no Color posible
                if(this.settings.sansEchec)
                {
                    // try set from proxy color (even if it break the rules)
                    int r = Random.Range(0,1);
                    if(r==1)
                    {
                        if(xTarget!=0)
                        {
                            if(list[xTarget-1, yTarget].Count!=0)
                            {
                                selectColor(list[xTarget, yTarget], list[xTarget-1, yTarget][0]);
                                return 1;
                            }
                            if(list[xTarget, yTarget-1].Count!=0)
                            {
                                selectColor(list[xTarget, yTarget], list[xTarget, yTarget-1][0]);
                                return 1;
                            }
                        }
                    
                    }else{
                        if(yTarget!=0)
                        {
                            if(list[xTarget, yTarget-1].Count!=0)
                            {
                                selectColor(list[xTarget, yTarget], list[xTarget, yTarget-1][0]);
                                return 1;
                            }
                            if(list[xTarget-1, yTarget].Count!=0)
                            {
                                selectColor(list[xTarget, yTarget], list[xTarget-1, yTarget][0]);
                                return 1;
                            }
                        }
                    }
                    
                }
                
                selectColor(list[xTarget, yTarget], Color.black);
                return 0;
            }

            if(countTarget == int.MaxValue)
            {
                // every color have been selected
                return 2;
            }
            
            if(settings.weightedRandomPixel)
            {
                List<Color> listColorRandom = new List<Color>();
                foreach (Color cloParcour in list[xTarget, yTarget])
                {
                    if(xTarget != 0)
                    {
                        if(list[xTarget-1, yTarget].Count == 1 && list[xTarget-1, yTarget][0] != Color.black)
                        {
                            for (int i = 0; i < linkNumProxyColbyCol[list[xTarget-1, yTarget][0]][cloParcour]; i++)
                            {
                                listColorRandom.Add(cloParcour);
                            }
                        }
                    }
                    if(xTarget != xL-1)
                    {
                        if(list[xTarget+1, yTarget].Count == 1 && list[xTarget+1, yTarget][0] != Color.black)
                        {
                            for (int i = 0; i < linkNumProxyColbyCol[list[xTarget+1, yTarget][0]][cloParcour]; i++)
                            {
                                listColorRandom.Add(cloParcour);
                            }
                            
                        }
                    }
                    if(yTarget != 0)
                    {
                        if(list[xTarget, yTarget-1].Count == 1 && list[xTarget, yTarget-1][0] != Color.black)
                        {
                            for (int i = 0; i < linkNumProxyColbyCol[list[xTarget, yTarget-1][0]][cloParcour]; i++)
                            {
                                listColorRandom.Add(cloParcour);
                            }
                            
                        }
                    }
                    if(yTarget != yL-1)
                    {
                        if(list[xTarget, yTarget+1].Count == 1 && list[xTarget, yTarget+1][0] != Color.black)
                        {
                            for (int i = 0; i < linkNumProxyColbyCol[list[xTarget, yTarget+1][0]][cloParcour]; i++)
                            {
                                listColorRandom.Add(cloParcour);
                            }
                            
                        }
                    }
                    listColorRandom.Add(cloParcour);
                }
                if(settings.ogPixelFavoritism)
                {
                    // multiply by a num the pixel frequency of the og img
                    Color ogPixel = settings.InputTexture.GetPixel(Mathf.RoundToInt((float)xTarget/(float)xL*(float)settings.InputTexture.width), Mathf.RoundToInt((float)yTarget/(float)yL*(float)settings.InputTexture.height));
                    int numToAdd = 0;
                    foreach (Color col in listColorRandom)
                    {
                        if(col==ogPixel)
                        {
                            numToAdd+=settings.ogPixelFavoritismIntensity;
                        }
                    }
                    for (int i = 0; i < numToAdd; i++)
                    {
                        listColorRandom.Add(ogPixel);
                    }
                }
                // select a random num
                int r = Mathf.RoundToInt(Random.Range(0, listColorRandom.Count));
                // select the color
                selectColor(list[xTarget, yTarget], listColorRandom[r]);
                return 1;
            } else {
                List<Color> listColorRandom = list[xTarget, yTarget];
                if(settings.ogPixelFavoritism)
                {
                    // multiply by a num the pixel frequency of the og img
                    Color ogPixel = settings.InputTexture.GetPixel(Mathf.RoundToInt((float)xTarget/(float)xL*(float)settings.InputTexture.width), Mathf.RoundToInt((float)yTarget/(float)yL*(float)settings.InputTexture.height));
                    int numToAdd = 0;
                    foreach (Color col in listColorRandom)
                    {
                        if(col==ogPixel)
                        {
                            numToAdd+=settings.ogPixelFavoritismIntensity;
                        }
                    }
                    for (int i = 0; i < numToAdd; i++)
                    {
                        listColorRandom.Add(ogPixel);
                    }
                }
                // select a random num
                int r = Mathf.RoundToInt(Random.Range(0, listColorRandom.Count));
                // select the color
                selectColor(list[xTarget, yTarget], listColorRandom[r]);
                return 1;
            }
            
        }
    }

}
