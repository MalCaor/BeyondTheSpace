using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridPointProxyMatrice
{
    [SerializeField]
    public GameObject[,,] matricePoint = new GameObject[3,3,3];
}
