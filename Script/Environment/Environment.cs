using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Environment
{
    // static var
    public static List<Environment> listEnvironmentGlobal = new List<Environment>();

    // pub var
    public string name;

    public Environment(string name)
    {
        this.name = name;
    }
}
