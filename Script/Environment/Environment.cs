using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <Summary> 
/// Abstract class parent of all Environments Class.<br/>
/// </Summary>
[System.Serializable]
public abstract class Environment
{
    /// <summary> Name of the Environment </summary>
    public string name;

    /// <Summary> 
    /// Create an Environment.<br/>
    /// </Summary>
    /// <param name="name">name of the new Environment.</param>
    public Environment(string name)
    {
        this.name = name;
    }
}
