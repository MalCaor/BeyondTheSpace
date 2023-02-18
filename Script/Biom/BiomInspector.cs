using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// custom Class Using
using SettingsBeyondTheSpace;
using EnvironmentBeyondTheSpace;

namespace BiomBeyondTheSpace 
{
    public class BiomInspector : MonoBehaviour
    {
        // Setting
        public BiomInspectorSettings biomInspectorSettings;

        public List<Biom>listBiom = new List<Biom>();

        public void SearchBiom()
        {
            // clear the current biom to update
            listBiom.Clear();
            // search all biom JSON File from biom foldier
            string JsonPath = Application.dataPath + "/BeyondTheSpace/Json/Biom/";
            DirectoryInfo dir = new DirectoryInfo(JsonPath);
            FileInfo[] info = dir.GetFiles("*.json");
            foreach (FileInfo f in info) 
            {
                // Read the file and create/add the biom to the list
                StreamReader reader = new StreamReader(f.FullName); 
                Biom b = JsonUtility.FromJson<Biom>(reader.ReadToEnd());
                listBiom.Add(b);
                reader.Close();
            }
        }
    }
}