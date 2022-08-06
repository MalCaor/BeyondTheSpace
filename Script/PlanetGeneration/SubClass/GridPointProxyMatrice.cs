using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridPointProxyMatrice : ISerializationCallbackReceiver
{
    [SerializeField]
    public GameObject[,,] matricePoint = new GameObject[3,3,3];

    // serialisation
    [SerializeField]
    List<GameObject> saveMatricePoint;
    [SerializeField]
    int xSave;
    [SerializeField]
    int zSave;
    [SerializeField]
    int ySave;
    public void OnBeforeSerialize()
    {
        saveMatricePoint = new List<GameObject>();
        xSave = matricePoint.GetLength(0);
        zSave = matricePoint.GetLength(1);
        ySave = matricePoint.GetLength(2);
        for (int x = 0; x < xSave; x++)
        {
            for (int z = 0; z < zSave; z++)
            {
                for (int y = 0; y < ySave; y++)
                {
                    saveMatricePoint.Add(matricePoint[x,z,y]);
                }
            }
        }
    }
    public void OnAfterDeserialize()
    {
        int i = 0;
        for (int x = 0; x < xSave; x++)
        {
            for (int z = 0; z < zSave; z++)
            {
                for (int y = 0; y < ySave; y++)
                {
                    matricePoint[x,z,y] = saveMatricePoint[i];
                    i++;
                }
            }
        }
    }
}
