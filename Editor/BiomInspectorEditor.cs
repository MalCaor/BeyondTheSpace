using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// custom Class Using
using BiomBeyondTheSpace;
using SettingsBeyondTheSpace;

[CustomEditor(typeof(BiomInspector))]
public class BiomInspectorEditor : Editor
{
    BiomInspector biomInspector;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSettingsEditor(biomInspector.biomInspectorSettings);

        
    }

    private void OnEnable()
	{
        biomInspector = (BiomInspector)target;
	}
    void DrawSettingsEditor(Object settings)
    {
        Editor editor = CreateEditor(settings);
        editor.OnInspectorGUI();
    }
}
