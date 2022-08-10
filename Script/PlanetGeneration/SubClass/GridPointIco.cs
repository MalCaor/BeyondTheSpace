using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPointIco : MonoBehaviour
{
    public void InitPoint(Vector3 position)
    {
        transform.position = position;
        gameObject.name = "Point : " + position.x + ", " + position.y + ", " + position.z;
    }
}
