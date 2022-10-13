using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// custom class using
using BuildingBeyondTheSpace;
using SettingsBeyondTheSpace;
using GridTileBeyondTheSpace;
using EnvironmentBeyondTheSpace;

namespace BiomBeyondTheSpace
{
    /// <Summary> 
    /// Handle the Generation of a Grid Collumn during the planet generation.<br/>
    /// Is represented by a Color Pixel.<br/>
    /// Black Pixel is the Default Error Biom.<br/>
    /// </Summary>
    public class Biom
    {
        /// <summary> static list of all Bioms </summary>
        public static List<Biom> allBioms = new List<Biom>();

        /// <summary> Color unique to this Biom </summary>
        Color colorID;
        /// <summary> Name of the Biom </summary>
        string nameBiom;

        public static Biom GetBiom(Color color)
        {
            return allBioms.Find((b) => b.colorID == color);
        }
        
        /// <summary> 
        /// Create a Biom with a Color ID
        /// </summary>
        /// <param name="color">Color ID of the Biom.</param>
        /// <param name="name">Name of the Biom.</param>
        public Biom(Color color, string name)
        {
            if (GetBiom(color) != null) throw new System.ArgumentException("Can not Create a Biom with the same Color");
            // set param
            this.colorID = color;
            this.nameBiom = name;

            // add new biom to the Biom list
            Biom.allBioms.Add(this);
        }
    }

}
