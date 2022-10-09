using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnvironmentBeyondTheSpace
{
    [System.Serializable]
    public class EnvironmentManager
    {
        public List<Environment> listEnvironment;

        public EnvironmentManager()
        {
            listEnvironment = new List<Environment>();
        }

        // SetColorTile for Debug puposes
        public Color GetColorTile()
        {
            if(listEnvironment.Find((x) => x.name=="water") != null)
            {
                return Color.blue;
            }
            if(listEnvironment.Find((x) => x.name=="earth") != null)
            {
                return Color.grey;
            }
            return Color.black;
        }
    }

}
