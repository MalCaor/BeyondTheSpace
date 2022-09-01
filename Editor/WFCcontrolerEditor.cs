using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WFCcontroler))]
public class WFCcontrolerEditor : Editor
{
    WFCcontroler controler;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Bitmap"))
        {
            controler.GenerateBitMapWFC();
        }
    }

    private void OnEnable()
	{
        controler = (WFCcontroler)target;
	}
}
