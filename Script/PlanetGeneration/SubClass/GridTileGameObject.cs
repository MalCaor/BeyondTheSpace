using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    public Mesh meshBoxTile;
    public MeshCollider meshCollider;

    // point mesh
    Vector3 pointMeshNO;
    Vector3 pointMeshNE;
    Vector3 pointMeshSO;
    Vector3 pointMeshSE;
    public Mesh meshTerrain;
    public MeshFilter meshFilterTerrain;
    public MeshRenderer meshRendererTerrain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTileLine()
    {
        gameObject.GetComponent<LineRenderer>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;
        if(gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void HideTileLine()
    {
        gameObject.GetComponent<LineRenderer>().enabled = false;
        gameObject.GetComponent<MeshCollider>().enabled = false;
        if(gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
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
        l.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        l.startColor = Color.black;
        l.endColor = Color.black;
        l.startWidth = 0.1f;
        l.endWidth = 0.1f;
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

    public void UpdatePointMesh()
    {
        pointMeshNO = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
        pointMeshNE = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
        pointMeshSO = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
        pointMeshSE = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);
    }

    public void drawMesh()
    {
        meshTerrain = new Mesh();
        // vert
        Vector3[] vert = new Vector3[4];
        vert[0] = transform.InverseTransformPoint(pointMeshNO);
        vert[1] = transform.InverseTransformPoint(pointMeshNE);
        vert[2] = transform.InverseTransformPoint(pointMeshSO);
        vert[3] = transform.InverseTransformPoint(pointMeshSE);
        meshTerrain.vertices = vert;
        int[] triangles = {
            2, 1, 0, //face front
            1, 2, 3
        };
        meshTerrain.triangles = triangles;
        if(gridTile.face != 0)
        {
            // reverse mesh (tech du chlag)
            meshTerrain.triangles = meshTerrain.triangles.Reverse().ToArray();
        }
        // mesh filter
        meshFilterTerrain = gameObject.GetComponent<MeshFilter>();
        if(meshFilterTerrain == null)
        {
            meshFilterTerrain = gameObject.AddComponent<MeshFilter>();
        }
        meshFilterTerrain.mesh = meshTerrain;
        // mesh Rend
        meshRendererTerrain = gameObject.GetComponent<MeshRenderer>();
        if(meshRendererTerrain == null)
        {
            meshRendererTerrain = gameObject.AddComponent<MeshRenderer>();
        }
        meshRendererTerrain.material = gridTile.gridTileManager.GetMaterialTile();
    }

    /// <summary>
    /// change the color of the Tile line
    /// </summary>
    public void SetLineColor(Color col)
    {
        LineRenderer l = gameObject.GetComponent<LineRenderer>();
        l.startColor = col;
        l.endColor = col;
    }

    /// <summary>
    /// change the color of the Tile line
    /// </summary>
    public void SetTileLayer(int layerInt)
    {
        gameObject.layer = layerInt;
    }

    /// <summary>
    /// change the color of the Tile in fonction of the car
    /// </summary>
    public void SetColTileAtri()
    {
        // call Grid Tile Mana to get color
        Color c = gridTile.gridTileManager.GetColorTile();
        SetLineColor(c);
    }

    /// <summary>
    /// mesh of the 8 vert of the Tile
    /// </summary>
    public void InitMeshBoxTile()
    {
        meshBoxTile = new Mesh();
        // vert
        Vector3[] vert = new Vector3[8];
        vert[0] = transform.InverseTransformPoint(gridTile.pointDownNE);
        vert[1] = transform.InverseTransformPoint(gridTile.pointDownNO);
        vert[2] = transform.InverseTransformPoint(gridTile.pointUpNO);
        vert[3] = transform.InverseTransformPoint(gridTile.pointUpNE);
        vert[4] = transform.InverseTransformPoint(gridTile.pointUpSE);
        vert[5] = transform.InverseTransformPoint(gridTile.pointUpSO);
        vert[6] = transform.InverseTransformPoint(gridTile.pointDownSO);
        vert[7] = transform.InverseTransformPoint(gridTile.pointDownSE);
        meshBoxTile.vertices = vert;
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
        meshBoxTile.triangles = triangles;
    }

    /// <summary>
    /// invisible Colide for tile
    /// </summary>
    public void InitMeshCollider()
    {
        meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = meshBoxTile;
        meshCollider.convex = true;
    }

    public void DrawGameObject()
    {
        if(gridTile.gridTileManager.tileTerrainType == 0)
        {
            // air
            DrawGameObjectAir();
        }
        if(gridTile.gridTileManager.tileTerrainType == 1)
        {
            // solid
            DrawGameObjectSolid();
        }
        if(gridTile.gridTileManager.tileTerrainType == 2)
        {
            // liquid
            DrawGameObjectLiquid();
        }
        if(gridTile.gridTileManager.tileTerrainType == 3)
        {
            // space
            DrawGameObjectSpace();
        }
    }

    void DrawGameObjectSolid()
    {
        try{
            if(ProxyTileNord.gridTileManager.tileTerrainType == 1 &&
            ProxyTileSud.gridTileManager.tileTerrainType == 1 &&
            ProxyTileOuest.gridTileManager.tileTerrainType == 1 &&
            ProxyTileEst.gridTileManager.tileTerrainType == 1 &&
            ProxyTileUp.gridTileManager.tileTerrainType == 1)
            {
                HideTileLine();
            } else{
                UpdatePointMesh();
                drawMesh();
                ShowTileLine();
            }
        }
        catch
        {
            UpdatePointMesh();
            drawMesh();
            ShowTileLine();
        }
    }

    void DrawGameObjectLiquid()
    {
        try{
            if(ProxyTileUp.gridTileManager.tileTerrainType == 0)
            {
                UpdatePointMesh();
                drawMesh();
                ShowTileLine();
            } else{
                HideTileLine();
            }
        }
        catch
        {
            UpdatePointMesh();
            drawMesh();
            ShowTileLine();
        }
    }

    void DrawGameObjectAir()
    {
        HideTileLine();
    }

    void DrawGameObjectSpace()
    {
        HideTileLine();
    }
}
