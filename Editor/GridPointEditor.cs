using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridPoint))]
public class GridPointEditor : Editor
{
    GridPoint point;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

    private void OnEnable()
	{
        point = (GridPoint)target;
	}
}
