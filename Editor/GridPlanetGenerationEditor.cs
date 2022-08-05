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
        DrawSettingsEditor(grid.planetSettings);

        if (GUILayout.Button("Generate Grid"))
        {
            grid.Init();
        }
        if (GUILayout.Button("Reload Proxy"))
        {
            grid.StartCoroutine(grid.ConnectPoints());
        }
    }
    private void OnEnable()
	{
        grid = (GridPlanetGeneration)target;
	}
    void DrawSettingsEditor(Object settings)
    {
        Editor editor = CreateEditor(settings);
        editor.OnInspectorGUI();
    }
}
