using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// custom Class Using
using PlanetGenerationBeyondTheSpace;
using SettingsBeyondTheSpace;

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
        if (GUILayout.Button("Generate Proxy Tile"))
        {
            grid.InitProxyTile();
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
