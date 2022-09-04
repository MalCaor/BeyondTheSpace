using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFCcontroler : MonoBehaviour
{
    // vars
    public Texture2D InputTexture;
    public string OutputTexture;
    public int newHeight;
    public int newWidth;
    public bool sansEchec;

    public void Init()
    {
        WaveFunctionCollapseTexture2D WFC = new WaveFunctionCollapseTexture2D();
        Texture2D text = WFC.run(InputTexture, newHeight, newWidth, sansEchec);
        // save texture
        byte[] pngBytes = text.EncodeToPNG();
        Debug.Log(Application.dataPath + OutputTexture);
        File.WriteAllBytes(Application.dataPath + OutputTexture);
    }
}
