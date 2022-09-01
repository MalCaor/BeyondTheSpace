using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFCcontroler : MonoBehaviour
{
    WaveFonctionCollapse WFC = new WaveFonctionCollapse();
    public Texture2D bitMap;
    public Texture2D bitMapOutPut;
    public int N;

    public void GenerateBitMapWFC()
    {
        OverlappingModel model = new OverlappingModel(bitMap, N, bitMap.width, bitMap.height, false, false, 0, false, Model.Heuristic.Entropy);
        int height = bitMap.height;
        int width = bitMap.width;
    }
}
