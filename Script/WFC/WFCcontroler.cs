using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFCcontroler : MonoBehaviour
{
    // vars
    public Texture2D InputTexture;
    public Texture2D OutputTexture;
    public int N;
    public int width;
    public int height;
    public bool periodicInput;
    public bool periodic; 
    public int symmetry; 
    public bool ground;
    public int seed;
    public int limit;

    public void Init()
    {
        //'OverlappingModel.OverlappingModel(Texture2D, int, int, int, bool, bool, int, bool, Model.Heuristic)'
        OverlappingModel omWFC = new OverlappingModel(InputTexture, N, width, height, periodicInput, periodic, symmetry, ground, Model.Heuristic.Entropy);
        //'Model.Run(int, int)'
        omWFC.Run(seed, limit);
        omWFC.Save(OutputTexture);
    }
}
