using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFCcontroler : MonoBehaviour
{
    WaveFonctionCollapse WFC = new WaveFonctionCollapse();
    public Texture2D bitMap;

    public void GenerateBitMapWFC()
    {
        WFC.GetAllColor(bitMap);
        return;
    }
}
