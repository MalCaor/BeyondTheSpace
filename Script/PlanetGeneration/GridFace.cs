using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFace
{
    // mesh
    Mesh mesh;
    // number of point per face
    int resolution;
    // where the face is facing (axis Y)
    Vector3 localUp;
    // other 2 vector dir
    Vector3 axisX;
    Vector3 axisZ;

    // constructor
    public GridFace(Mesh m, int r, Vector3 lUp)
    {
        // set local var from para
        this.mesh = m;
        this.resolution = r;
        this.localUp = lUp;

        // init the other 2 vec
        axisX = new Vector3(localUp.y, localUp.z, localUp.x);
        axisZ = Vector3.Cross(localUp, axisX);
    }

    // construct the face
    public void ConstructFace()
    {
        Vector3[] vertices = new Vector3[resolution * resolution];
        int[] triangles = new int[(resolution-1) * (resolution-1) * 6];
        int triIndex = 0;

        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                // create point
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution -1);
                Vector3 pointOnUnitCube = localUp + (percent.x - 0.5f) * 2 * axisX + (percent.y - 0.5f) * 2 * axisZ;
                vertices[i] = pointOnUnitCube;

                // create triangle
                if(x != resolution -1 && y != resolution -1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex+1] = i + resolution + 1;
                    triangles[triIndex+2] = i + resolution;

                    triangles[triIndex+3] = i;
                    triangles[triIndex+4] = i +1;
                    triangles[triIndex+5] = i + resolution + 1;
                    triIndex += 6;
                }
            }
        }

        // mesh init
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
}
