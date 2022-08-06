using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridMatrice : ISerializationCallbackReceiver
{
    // list of point in a grid shorted by Face, x, z, y
    public GameObject[,,,] points;

    // serialisation
    [SerializeField]
    List<GameObject> saveMatricePoint;
    [SerializeField]
    int faceSave;
    [SerializeField]
    int xSave;
    [SerializeField]
    int zSave;
    [SerializeField]
    int ySave;
    public void OnBeforeSerialize()
    {
        saveMatricePoint = new List<GameObject>();
        faceSave = points.GetLength(0);
        xSave = points.GetLength(1);
        zSave = points.GetLength(2);
        ySave = points.GetLength(3);
        for (int face = 0; face < faceSave; face++)
        {
            for (int x = 0; x < xSave; x++)
            {
                for (int z = 0; z < zSave; z++)
                {
                    for (int y = 0; y < ySave; y++)
                    {
                        saveMatricePoint.Add(points[face, x, z, y]);
                    }
                }
            }
        }
    }
    public void OnAfterDeserialize()
    {
        points = new GameObject[faceSave, xSave, zSave, ySave];
        int i = 0;
        for (int face = 0; face < faceSave; face++)
        {
            for (int x = 0; x < xSave; x++)
            {
                for (int z = 0; z < zSave; z++)
                {
                    for (int y = 0; y < ySave; y++)
                    {
                        points[face, x, z, y] = saveMatricePoint[i];
                        i++;
                    }
                }
            }
        }
    }
}
