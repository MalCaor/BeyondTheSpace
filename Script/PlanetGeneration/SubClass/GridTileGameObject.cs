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

    public void InitMeshBoxTile()
    {
        meshBoxTile = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshBoxTile.mesh = mesh;
        Vector3[] vert = new Vector3[8];
        vert[0] = gridTile.pointDownNO;
        vert[1] = gridTile.pointDownNE;
        vert[2] = gridTile.pointDownSE;
        vert[3] = gridTile.pointDownSO;
        vert[4] = gridTile.pointUpNO;
        vert[5] = gridTile.pointUpNE;
        vert[6] = gridTile.pointUpSE;
        vert[7] = gridTile.pointUpSO;
        mesh.vertices = vert;
    }
}
