using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// custom Class Using
using WFCBeyondTheSpace;

[CustomEditor(typeof(WFCcontroler))]
public class WFCeditor : Editor
{
    WFCcontroler wfc;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        DrawSettingsEditor(wfc.settings);
        if (GUILayout.Button("Generate WFC"))
        {
            wfc.Init();
        }
    }

    private void OnEnable()
	{
        wfc = (WFCcontroler)target;
	}
    void DrawSettingsEditor(Object settings)
    {
        Editor editor = CreateEditor(settings);
        editor.OnInspectorGUI();
    }
}
