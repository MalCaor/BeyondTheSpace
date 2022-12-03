using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SettingsBeyondTheSpace
{
    [CreateAssetMenu()]
    public class TerrainPlanetGenerationSetting : ScriptableObject
    {
        // seting for terrain generation
        public bool water = true;
        public int waterLevel;
        public int SeedTerrainGeneration = 1;
        // rougness of the noise
        public float roughnessNoiseTerrainElevation = 1f;

        /// <summary> Text map of Bioms Face Nord </summary>
        public Texture2D mapFaceNord;
        /// <summary> Text map of Bioms Face Est </summary>
        public Texture2D mapFaceEst;
        /// <summary> Text map of Bioms Face Ouest </summary>
        public Texture2D mapFaceOuest;
        /// <summary> Text map of Bioms Face Front </summary>
        public Texture2D mapFaceFront;
        /// <summary> Text map of Bioms Face Back </summary>
        public Texture2D mapFaceBack;
        /// <summary> Text map of Bioms Face Sud </summary>
        public Texture2D mapFaceSud;
    }

}
