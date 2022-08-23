using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileGameObject : MonoBehaviour
{
    // public var
    public GridTile gridTile;
    // Proxy GridTile
    public GridTile ProxyTileNord;
    public GridTile ProxyTileSud;
    public GridTile ProxyTileOuest;
    public GridTile ProxyTileEst;
    public GridTile ProxyTileUp;
    public GridTile ProxyTileDown;

    // mesh
    public MeshFilter meshBoxTile;
    public MeshCollider meshCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateProxy()
    {
        if(gridTile.ProxyTileNord!=null)
        {
            this.ProxyTileNord = gridTile.ProxyTileNord;
        }
        if(gridTile.ProxyTileSud!=null)
        {
            this.ProxyTileSud = gridTile.ProxyTileSud;
        }
        if(gridTile.ProxyTileOuest!=null)
        {
            this.ProxyTileOuest = gridTile.ProxyTileOuest;
        }
        if(gridTile.ProxyTileEst!=null)
        {
            this.ProxyTileEst = gridTile.ProxyTileEst;
        }
        if(gridTile.ProxyTileUp!=null)
        {
            this.ProxyTileUp = gridTile.ProxyTileUp;
        }
        if(gridTile.ProxyTileDown!=null)
        {
            this.ProxyTileDown = gridTile.ProxyTileDown;
        }
    }

    public void InitLineTile()
    {
        LineRenderer l = gameObject.AddComponent<LineRenderer>();
        l.material = (Material)Resources.Load("Material/LineTileMat");
        l.startColor = Color.black;
        l.endColor = Color.black;
        l.startWidth = 0.01f;
        l.endWidth = 0.01f;
        l.positionCount = 10;
        l.SetPosition(0, gridTile.pointDownNO);
        l.SetPosition(1, gridTile.pointDownNE);
        l.SetPosition(2, gridTile.pointDownSE);
        l.SetPosition(3, gridTile.pointDownSO);
        l.SetPosition(4, gridTile.pointDownNO);
        // up
        l.SetPosition(5, gridTile.pointUpNO);
        l.SetPosition(6, gridTile.pointUpNE);
        l.SetPosition(7, gridTile.pointUpSE);
        l.SetPosition(8, gridTile.pointUpSO);
        l.SetPosition(9, gridTile.pointUpNO);
    }

    /// <summary>
    /// mesh of the 8 vert of the Tile
    /// </summary>
    public void InitMeshBoxTile()
    {
        meshBoxTile = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshBoxTile.mesh = mesh;
        // vert
        Vector3[] vert = new Vector3[8];
        vert[0] = gridTile.pointDownNE;
        vert[1] = gridTile.pointDownNO;
        vert[2] = gridTile.pointUpNO;
        vert[3] = gridTile.pointUpNE;
        vert[4] = gridTile.pointUpSE;
        vert[5] = gridTile.pointUpSO;
        vert[6] = gridTile.pointDownSO;
        vert[7] = gridTile.pointDownSE;
        mesh.vertices = vert;
        int[] triangles = {
            0, 2, 1, //face front
            0, 3, 2,
            2, 3, 4, //face top
            2, 4, 5,
            1, 2, 5, //face right
            1, 5, 6,
            0, 7, 4, //face left
            0, 4, 3,
            5, 4, 7, //face back
            5, 7, 6,
            0, 6, 7, //face bottom
            0, 1, 6
        };
        mesh.triangles = triangles;
    }

    /// <summary>
    /// invisible Colide for tile
    /// </summary>
    public void InitMeshCollider()
    {
        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshBoxTile.sharedMesh;
    }
}
