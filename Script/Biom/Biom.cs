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
    public class Biom : BiomInterface
    {
        /// <summary> static list of all Bioms </summary>
        public static List<Biom> allBioms = new List<Biom>();

        /// <summary> Color unique to this Biom </summary>
        Color colorID;
        /// <summary> Name of the Biom </summary>
        string nameBiom;
        /// <summary> Dic of Env </summary>
        Dictionary<float, Environment> dicEnv = new Dictionary<float, Environment>();

        /// <summary> 
        /// return Biom corresponding to the color id
        /// if list biom is empty search json biom (tmp)
        /// </summary>
        /// <param name="color">Color ID of the Biom.</param>
        public static Biom GetBiom(Color color)
        {
            if (allBioms.Count == 0)
            {
                // there is no biom in the biom list
                // search biom json
                fillBiomListFromJSON();
            }
            return allBioms.Find((b) => b.colorID == color);
        }

        /// <summary> 
        /// fill allBiom with biom in json file
        /// </summary>
        private static void fillBiomListFromJSON()
        {
            /// TODO : search and deserialize biom
            /// TODO : have the json files loc in a param or something
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

        // inherited interface
        public List<Environment> GetEnvironments(GridTile gridTile, TerrainPlanetGenerationSetting terrainSetting)
        {
            return null;
        }

        public List<Building> GetBuildings(GridTile gridTile, TerrainPlanetGenerationSetting terrainSetting)
        {
            return null;
        }
    }

}
