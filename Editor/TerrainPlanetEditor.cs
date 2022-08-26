using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainPlanetGeneration))]
public class TerrainPlanetEditor : Editor
{
    TerrainPlanetGeneration terrain;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSettingsEditor(terrain.terrainSetting);

        if (GUILayout.Button("Generate terrain"))
        {
            terrain.Init();
        }
    }
    private void OnEnable()
	{
        terrain = (TerrainPlanetGeneration)target;
	}
    void DrawSettingsEditor(Object settings)
    {
        Editor editor = CreateEditor(settings);
        editor.OnInspectorGUI();
    }
}
