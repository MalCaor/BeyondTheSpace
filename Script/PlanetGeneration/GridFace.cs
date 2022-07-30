using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFace : MonoBehaviour
{
    // parent transform
    Transform transformParent;
    // point
    GameObject[] points;
    // number of point per face
    int resolution;
    // where the face is facing (axis Y)
    Vector3 localUp;
    // other 2 vector dir
    Vector3 axisX;
    Vector3 axisZ;

    // constructor
    public void ConstructGrid(Transform t, int r, Vector3 lUp)
    {
        // set local var from para
        this.transformParent = t;
        this.resolution = r;
        this.localUp = lUp;

        // init the other 2 vec
        axisX = new Vector3(localUp.y, localUp.z, localUp.x);
        axisZ = Vector3.Cross(localUp, axisX);
    }

    // construct the face
    public void ConstructFace()
    {
        GameObject[] points = new GameObject[resolution * resolution];
        for (int i = 0; i < resolution * resolution; i++)
        {
            points[i] = new GameObject();
            points[i].transform.SetParent(this.transformParent);
        }

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                // create point
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution -1);
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisX + (percent.y - 0.5f) * 2 * axisZ;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                Instantiate(points[i], pointOnUnitSphere, Quaternion.identity);
            }
        }
    }
}
