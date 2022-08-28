using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentGlobalGeneration
{
    public static void InitAllEnvironment()
    {
        Environment.listEnvironmentGlobal.Add(new Environment("water"));
        Environment.listEnvironmentGlobal.Add(new Environment("air"));
        Environment.listEnvironmentGlobal.Add(new Environment("earth"));
    }
}
