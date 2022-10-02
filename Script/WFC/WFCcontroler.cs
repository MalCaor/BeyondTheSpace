using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class WFCcontroler : MonoBehaviour
{
    // vars
    public string OutputTexture;
    public WFCText2DSettings settings;

    public void Init()
    {
        WaveFunctionCollapseTexture2D WFC = new WaveFunctionCollapseTexture2D();
        Texture2D text = WFC.run(settings);
        // save texture
        byte[] pngBytes = text.EncodeToPNG();
        if(pngBytes!=null)
        {
            File.WriteAllBytes(Application.dataPath + OutputTexture, pngBytes);
        }
    }
}
