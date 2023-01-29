using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SettingsBeyondTheSpace
{
    [CreateAssetMenu()]
    public class BiomCreatorSettings : ScriptableObject
    {
        /// <summary> Color unique to this Biom </summary>
        public Color colorID;
        /// <summary> Name of the Biom </summary>
        public string nameBiom;
    }
}
