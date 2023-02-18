using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

// custom Class Using
using SettingsBeyondTheSpace;

namespace BiomBeyondTheSpace 
{
    ///<summary> BiomSingleton Class </summary>
    [CreateAssetMenu]
    public class BiomSingleton : ScriptableSingleton<BiomSingleton>
    {
        ///<summary> List of all Existing Biom </summary>
        public List<Biom>listBiom = new List<Biom>();

        public void UpdateBiom()
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

        public List<Biom> getListBiom()
        {
            return listBiom;
        }
    }
}