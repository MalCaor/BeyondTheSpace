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
            for (int x = 0; x < point.gridPointProxyMatrice.matricePoint.GetLength(0); x++)
            {
                EditorGUILayout.LabelField("X : " + (x-1));
                EditorGUI.indentLevel++;
                for (int z = 0; z < point.gridPointProxyMatrice.matricePoint.GetLength(1); z++)
                {
                    EditorGUILayout.LabelField("Z : " + (z-1));
                    EditorGUI.indentLevel++;
                    for (int y = 0; y < point.gridPointProxyMatrice.matricePoint.GetLength(2); y++)
                    {
                        GameObject p = (GameObject)EditorGUILayout.ObjectField("Point : " + (x-1) + ", " + (z-1) + ", " + (y-1), point.gridPointProxyMatrice.matricePoint[x,z,y], typeof(GameObject), true);
                    }
                    EditorGUI.indentLevel--;
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.indentLevel--;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}