using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


// custom Class Using
using SettingsBeyondTheSpace;
using EnvironmentBeyondTheSpace;

namespace BiomBeyondTheSpace 
{
    public class BiomCreator : MonoBehaviour
    {
        // Setting
        public BiomCreatorSettings biomCreatorSettings;

        public void CreateBiom()
        {
            Biom biom = new Biom(this.biomCreatorSettings.colorID, this.biomCreatorSettings.nameBiom);
            string jsonBiom = JsonUtility.ToJson(biom);
            string pathBiom = Application.dataPath + "/BeyondTheSpace/Json/Biom/" + this.biomCreatorSettings.nameBiom + ".json";
            StreamWriter sw = File.CreateText(pathBiom);
            sw.Write(jsonBiom);
            sw.Close();
        }
    }
}