using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GridPoint))]
public class GridPointEditor : Editor
{
    public bool showGridPoint = true;

    public override void OnInspectorGUI()
    {
        GridPoint point = (GridPoint)target;
        EditorGUILayout.Space();

        showGridPoint = EditorGUILayout.BeginFoldoutHeaderGroup(showGridPoint, "Grid Proxy ALL Point");
        if(showGridPoint) {
            EditorGUI.indentLevel++;
            for (int x = 0; x < point.matricePoint.GetLength(0); x++)
            {
                bool showGridPointX = EditorGUILayout.Foldout(showGridPoint, "Grid Axe X : (" + x +")");
                if(showGridPointX) {
                    EditorGUI.indentLevel++;
                    for (int z = 0; z < point.matricePoint.GetLength(1); z++)
                    {
                        bool showGridPointZ = EditorGUILayout.Foldout(showGridPoint, "Grid Axe Z : (" + z +")");
                        if(showGridPointZ) {
                            EditorGUI.indentLevel++;
                            for (int y = 0; y < point.matricePoint.GetLength(2); y++)
                            {
                                GameObject p = (GameObject)EditorGUILayout.ObjectField("Point : " + (x-1) + ", " + (z-1) + ", " + (y-1), point.matricePoint[x,z,y], typeof(GameObject), true);
                            }
                            EditorGUI.indentLevel--;
                        }
                    }
                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}