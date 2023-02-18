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

        public BiomSingleton biomSingleton;
        public List<Biom>listBiom = new List<Biom>(); 

        public void SearchBiom()
        {
            biomSingleton.UpdateBiom();
            this.listBiom = biomSingleton.getListBiom();
        }
    }
}