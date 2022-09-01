using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WFCcontroler))]
public class WFCeditor : Editor
{
    WFCcontroler wfc;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate WFC"))
        {
            wfc.Init();
        }
    }

    private void OnEnable()
	{
        wfc = (WFCcontroler)target;
	}
}
