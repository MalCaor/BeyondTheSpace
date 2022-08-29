using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

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

    // point mesh coin
    Vector3 pointMeshNO;
    Vector3 pointMeshNE;
    Vector3 pointMeshSO;
    Vector3 pointMeshSE;
    // mid point mesh
    Vector3 pointMeshMidN;
    Vector3 pointMeshMidE;
    Vector3 pointMeshMidS;
    Vector3 pointMeshMidO;
    // mid mid
    Vector3 pointMeshMid;

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

    public void UpdatePointMeshSolid()
    {
        int etatN;
        int etatE;
        int etatS;
        int etatO;
        int etatU;
        int etatD;
        // get voisin etat
        if(ProxyTileNord != null)
        {
            etatN = ProxyTileNord.gridTileManager.tileTerrainType;
            if(etatN != 1){
                etatN = 0;
            }
        } else {
            etatN = 0;
        }
        if(ProxyTileEst != null)
        {
            etatE = ProxyTileEst.gridTileManager.tileTerrainType;
            if(etatE != 1){
                etatE = 0;
            }
        } else {
            etatE = 0;
        }
        if(ProxyTileSud != null)
        {
            etatS = ProxyTileSud.gridTileManager.tileTerrainType;
            if(etatS != 1){
                etatS = 0;
            }
        } else {
            etatS = 0;
        }
        if(ProxyTileOuest != null)
        {
            etatO = ProxyTileOuest.gridTileManager.tileTerrainType;
            if(etatO != 1){
                etatO = 0;
            }
        } else {
            etatO = 0;
        }
        if(ProxyTileUp != null)
        {
            etatU = ProxyTileUp.gridTileManager.tileTerrainType;
            if(etatU != 1){
                etatU = 0;
            }
        } else {
            etatU = 0;
        }
        if(ProxyTileDown != null)
        {
            etatD = ProxyTileDown.gridTileManager.tileTerrainType;
            if(etatD != 1){
                etatD = 0;
            }
        } else {
            etatD = 0;
        }

        pointMeshNO = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
        pointMeshNE = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
        pointMeshSO = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
        pointMeshSE = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);

        pointMeshMidN = Vector3.Lerp(pointMeshNO, pointMeshNE, 0.5f);
        pointMeshMidE = Vector3.Lerp(pointMeshNE, pointMeshSE, 0.5f);
        pointMeshMidS = Vector3.Lerp(pointMeshSO, pointMeshSE, 0.5f);
        pointMeshMidO = Vector3.Lerp(pointMeshNO, pointMeshSO, 0.5f);

        pointMeshMid = transform.position;
        
        // switch etat
        int numEtat = Convert.ToInt32(etatN.ToString()+etatE.ToString()+etatS.ToString()+etatO.ToString()+etatD.ToString()+etatU.ToString(), 2);
        Debug.Log(numEtat);
        switch (numEtat)
        {
            // N / E / S / O / D / U
            case(0):
            break;
            case(1):
            break;
            case(2):
            break;
            case(3):
            break;
            case(4):
            break;
            case(5):
            break;
            case(6):
            break;
            case(7):
            break;
            case(8):
            break;
            case(9):
            break;
            case(10):
            break;
            case(11):
            break;
            case(12):
            break;
            case(13):
            break;
            case(14):
            break;
            case(15):
            break;
            case(16):
            break;
            case(17):
            break;
            case(18):
            break;
            case(19):
            break;
            case(20):
            break;
            case(21):
            break;
            case(22):
            break;
            case(23):
            break;
            case(24):
            break;
            case(25):
            break;
            case(26):
            break;
            case(27):
            break;
            case(28):
            break;
            case(29):
            break;
            case(30):
            break;
            case(31):
            break;
            case(32):
            break;
            case(33):
            break;
            case(34):
            break;
            case(35):
            break;
            case(36):
            break;
            case(37):
            break;
            case(38):
            break;
            case(39):
            break;
            case(40):
            break;
            case(41):
            break;
            case(42):
            break;
            case(43):
            break;
            case(45):
            break;
            case(46):
            break;
            case(47):
            break;
            case(48):
            break;
            case(49):
            break;
            case(50):
            break;
            case(51):
            break;
            case(52):
            break;
            case(53):
            break;
            case(54):
            break;
            case(55):
            break;
            case(56):
            break;
            case(57):
            break;
            case(58):
            break;
            case(59):
            break;
            case(60):
            break;
            case(61):
            break;
            case(62):
            break;
            case(63):
            break;
            default:
            break;
        }
    }

    public void UpdatePointMeshLiquid()
    {
        
        pointMeshNO = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
        pointMeshNE = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
        pointMeshSO = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
        pointMeshSE = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);

        pointMeshMidN = Vector3.Lerp(pointMeshNO, pointMeshNE, 0.5f);
        pointMeshMidE = Vector3.Lerp(pointMeshNE, pointMeshSE, 0.5f);
        pointMeshMidS = Vector3.Lerp(pointMeshSO, pointMeshSE, 0.5f);
        pointMeshMidO = Vector3.Lerp(pointMeshNO, pointMeshSO, 0.5f);

        pointMeshMid = transform.position;
    }

    public void drawMesh()
    {
        meshTerrain = new Mesh();
        // vert
        Vector3[] vert = new Vector3[9];
        vert[0] = transform.InverseTransformPoint(pointMeshNO);
        vert[1] = transform.InverseTransformPoint(pointMeshNE);
        vert[2] = transform.InverseTransformPoint(pointMeshSO);
        vert[3] = transform.InverseTransformPoint(pointMeshSE);
        // mid p
        vert[4] = transform.InverseTransformPoint(pointMeshMidN);
        vert[5] = transform.InverseTransformPoint(pointMeshMidE);
        vert[6] = transform.InverseTransformPoint(pointMeshMidS);
        vert[7] = transform.InverseTransformPoint(pointMeshMidO);
        // mid mid
        vert[8] = transform.InverseTransformPoint(pointMeshMid);
        meshTerrain.vertices = vert;
        int[] triangles = {
            //face NO
            7, 0, 4, 
            7, 4, 8,
            //face NE
            8, 4, 1, 
            8, 1, 5,
            //face SO
            2, 7, 8, 
            2, 8, 6,
            //face SE
            6, 8, 5, 
            6, 5, 3
        };
        meshTerrain.triangles = triangles;
        if(gridTile.face == 0)
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

    public float GetMidPointProxyPos()
    {
        float retour = 0.5f;
        if(gridTile.gridTileManager.tileTerrainType == 0)
        {
            // i'm air
            return (float)0;
        }
        if(gridTile.gridTileManager.tileTerrainType == 2)
        {
            // I'm Water
            return (float)0;
        }
        if(ProxyTileUp != null)
        {
            if(ProxyTileUp.gridTileManager.tileTerrainType == 1)
            {
                return 1f;
            }
        }
        return retour;
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
                UpdatePointMeshSolid();
                drawMesh();
                ShowTileLine();
            }
        }
        catch
        {
            UpdatePointMeshSolid();
            drawMesh();
            ShowTileLine();
        }
    }

    void DrawGameObjectLiquid()
    {
        try{
            if(ProxyTileUp.gridTileManager.tileTerrainType == 0)
            {
                UpdatePointMeshLiquid();
                drawMesh();
                ShowTileLine();
            } else{
                HideTileLine();
            }
        }
        catch
        {
            UpdatePointMeshLiquid();
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
