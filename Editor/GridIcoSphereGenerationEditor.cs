using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridIcoSphereGeneration))]
public class GridIcoSphereGenerationEditor : Editor
{
    GridIcoSphereGeneration grid;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSettingsEditor(grid.planetSettings);

        if (GUILayout.Button("Generate Grid"))
        {
            grid.InitGrid();
        }
    }
    private void OnEnable()
	{
        grid = (GridIcoSphereGeneration)target;
	}
    void DrawSettingsEditor(Object settings)
    {
        Editor editor = CreateEditor(settings);
        editor.OnInspectorGUI();
    }
}
