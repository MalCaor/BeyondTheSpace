using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridPlanetGeneration))]
public class GridPlanetGenerationEditor : Editor
{
    GridPlanetGeneration grid;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Grid"))
        {
            grid.Init();
        }
    }
    private void OnEnable()
	{
        grid = (GridPlanetGeneration)target;
	}
}
