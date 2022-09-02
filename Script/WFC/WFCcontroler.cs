using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFCcontroler : MonoBehaviour
{
    // vars
    public Texture2D InputTexture;
    public Texture2D OutputTexture;
    public int newHeight;
    public int newWidth;

    public void Init()
    {
        WaveFunctionCollapseTexture2D WFC = new WaveFunctionCollapseTexture2D();
        WFC.run(InputTexture, OutputTexture, newHeight, newWidth);
    }
}
