using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuildingBeyondTheSpace
{
    /// <Summary> 
    /// Abstract class parent of all Buildings Class.<br/>
    /// </Summary>
    [System.Serializable]
    public abstract class Building
    {
        /// <summary> Name of the Buildings </summary>
        public string name;

        /// <Summary> 
        /// Create an Buildings.<br/>
        /// </Summary>
        /// <param name="name">name of the new Buildings.</param>
        public Building(string name)
        {
            this.name = name;
        }
    }

}
