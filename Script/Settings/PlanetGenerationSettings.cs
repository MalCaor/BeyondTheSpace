using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SettingsBeyondTheSpace
{
    [CreateAssetMenu()]
    public class PlanetGenerationSettings : ScriptableObject
    {
        // instan the test point or the real one
        public bool PointTest = false;
        // public bool sphere or not
        public bool Sphere = true;
        // size of the planet
        public float radius = 1;
        // resolution
        [Range(1, 100)]
        public int resolution = 10;
        // height of the "croute terrestre"
        [Range(1, 10)]
        public int height = 1;
        // height of a Tile
        [Range(0.01f, 1f)]
        public float tileHeight = 0.1f;
        // draw or not face
        public bool faceDown = true;
        public bool faceBack = true;
        public bool faceUp = true;
        public bool faceForward = true;
        public bool faceLeft = true;
        public bool faceRight = true;
    }

}
