using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary> 
/// Handle the Generation of a Grid Collumn during the planet generation.<br/>
/// Is represented by a Color Pixel.<br/>
/// Black Pixel is the Default Error Biom.<br/>
/// </Summary>
public abstract class Biom
{
    /// <summary> Color unique to this Biom </summary>
    Color colorID;
    /// <summary> Name of the Biom </summary>
    string nameBiom;
    
    /// <summary> 
    /// Create a Biom with a Color ID
    /// </summary>
    /// <param name="color">Color ID of the Biom.</param>
    /// <param name="name">Name of the Biom.</param>
    public Biom(Color color, string name)
    {
        // set param
        this.colorID = color;
        this.nameBiom = name;
    }
}
