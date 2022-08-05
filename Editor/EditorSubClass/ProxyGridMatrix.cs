using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProxyGridMatrix
{
    [System.Serializable]
    public struct rowData
    {
        public GameObject[] row;
    }

    public rowData[] rows = new rowData[3];
}
