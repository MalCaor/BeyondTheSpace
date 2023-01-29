using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// custom Class Using
using BiomBeyondTheSpace;
using SettingsBeyondTheSpace;

[CustomEditor(typeof(BiomCreator))]
public class BiomCreatoreditor : Editor
{
    BiomCreator biomCreator;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSettingsEditor(biomCreator.biomCreatorSettings);
    }

    private void OnEnable()
	{
        biomCreator = (BiomCreator)target;
	}
    void DrawSettingsEditor(Object settings)
    {
        Editor editor = CreateEditor(settings);
        editor.OnInspectorGUI();
    }
}
