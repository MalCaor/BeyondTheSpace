using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GridTileGameObject : MonoBehaviour
{
    // public var
    public GridTile gridTile;

    // mesh
    public Mesh meshBoxTile;
    public MeshCollider meshCollider;

    Vector3[] listPointMesh;

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
    
    // uv
    Vector2[] uvMeshTerrain;

    public void ShowTileLine()
    {
        gameObject.GetComponent<LineRenderer>().enabled = true;
    }

    public void HideTileLine()
    {
        gameObject.GetComponent<LineRenderer>().enabled = false;
    }
    
    public void ShowMeshTerrain()
    {
        gameObject.SetActive(true);
        gameObject.GetComponent<MeshCollider>().enabled = true;
        if(gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void HideMeshTerrain()
    {
        gameObject.GetComponent<MeshCollider>().enabled = false;
        if(gameObject.GetComponent<MeshRenderer>() != null)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
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
        l.enabled = false;
    }

    public void DrawMeshSolid()
    {
        // init mesh
        meshTerrain = new Mesh();

        int etatN;
        int etatE;
        int etatS;
        int etatO;
        int etatU;
        int etatD;
        // get voisin etat
        if(gridTile.ProxyTileNord != null)
        {
            etatN = gridTile.ProxyTileNord.gridTileManager.tileTerrainType;
            if(etatN != 1){
                etatN = 0;
            }
        } else {
            etatN = 0;
        }
        if(gridTile.ProxyTileEst != null)
        {
            etatE = gridTile.ProxyTileEst.gridTileManager.tileTerrainType;
            if(etatE != 1){
                etatE = 0;
            }
        } else {
            etatE = 0;
        }
        if(gridTile.ProxyTileSud != null)
        {
            etatS = gridTile.ProxyTileSud.gridTileManager.tileTerrainType;
            if(etatS != 1){
                etatS = 0;
            }
        } else {
            etatS = 0;
        }
        if(gridTile.ProxyTileOuest != null)
        {
            etatO = gridTile.ProxyTileOuest.gridTileManager.tileTerrainType;
            if(etatO != 1){
                etatO = 0;
            }
        } else {
            etatO = 0;
        }
        if(gridTile.ProxyTileUp != null)
        {
            etatU = gridTile.ProxyTileUp.gridTileManager.tileTerrainType;
            if(etatU != 1){
                etatU = 0;
            }
        } else {
            etatU = 0;
        }
        if(gridTile.ProxyTileDown != null)
        {
            etatD = gridTile.ProxyTileDown.gridTileManager.tileTerrainType;
            if(etatD != 1){
                etatD = 0;
            }
        } else {
            etatD = 0;
        }

        List<Vector3> listVert = new List<Vector3>();
        int numFace = 0;

        // put face in mid if only up or down
        if((etatU == 1 && etatD == 0) || (etatU == 0 && etatD == 1))
        {
            Vector3 NO = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
            Vector3 NE = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
            Vector3 SO = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
            Vector3 SE = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);
            listVert.Add(NO);
            listVert.Add(NE);
            listVert.Add(SO);
            listVert.Add(SE);
            numFace++;
        }

        // set face in the air
        if(etatN == 0)
        {
            Vector3 NO;
            Vector3 NE;
            Vector3 SO;
            Vector3 SE;
            if(etatU == 1)
            {
                NO = gridTile.pointUpNO;
                NE = gridTile.pointUpNE;
            } else {
                NO = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
                NE = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
            }
            if(etatD == 1)
            {
                SO = gridTile.pointDownNO;
                SE = gridTile.pointDownNE;
            } else {
                SO = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
                SE = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
            }
            listVert.Add(NE);
            listVert.Add(NO);
            listVert.Add(SE);
            listVert.Add(SO);
            numFace++;
        }
        if(etatS == 0)
        {
            Vector3 NO;
            Vector3 NE;
            Vector3 SO;
            Vector3 SE;
            if(etatU == 1)
            {
                NO = gridTile.pointUpSO;
                NE = gridTile.pointUpSE;
            } else {
                NO = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
                NE = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);
            }
            if(etatD == 1)
            {
                SO = gridTile.pointDownSO;
                SE = gridTile.pointDownSE;
            } else {
                SO = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
                SE = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);
            }
            listVert.Add(NO);
            listVert.Add(NE);
            listVert.Add(SO);
            listVert.Add(SE);
            numFace++;
        }
        if(etatE == 0)
        {
            Vector3 NO;
            Vector3 NE;
            Vector3 SO;
            Vector3 SE;
            if(etatU == 1)
            {
                NO = gridTile.pointUpSE;
                NE = gridTile.pointUpNE;
            } else {
                NO = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);
                NE = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
            }
            if(etatD == 1)
            {
                SO = gridTile.pointDownSE;
                SE = gridTile.pointDownNE;
            } else {
                SO = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);
                SE = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
            }
            listVert.Add(NO);
            listVert.Add(NE);
            listVert.Add(SO);
            listVert.Add(SE);
            numFace++;
        }
        if(etatO == 0)
        {
            Vector3 NO;
            Vector3 NE;
            Vector3 SO;
            Vector3 SE;
            if(etatU == 1)
            {
                NO = gridTile.pointUpSO;
                NE = gridTile.pointUpNO;
            } else {
                NO = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
                NE = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
            }
            if(etatD == 1)
            {
                SO = gridTile.pointDownSO;
                SE = gridTile.pointDownNO;
            } else {
                SO = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
                SE = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
            }
            listVert.Add(NE);
            listVert.Add(NO);
            listVert.Add(SE);
            listVert.Add(SO);
            numFace++;
        }
        // set face earth (corner)
        if(etatN == 1)
        {
            if(gridTile.ProxyTileNord.solidFaceUp() == 1)
            {
                // draw corner
                Vector3 NO;
                Vector3 NE;
                Vector3 SO;
                Vector3 SE;
                NO = gridTile.pointUpNO;
                NE = gridTile.pointUpNE;
                SO = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
                SE = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
                listVert.Add(NO);
                listVert.Add(NE);
                listVert.Add(SO);
                listVert.Add(SE);
                numFace++;
            }
        }
        if(etatS == 1)
        {
            if(gridTile.ProxyTileSud.solidFaceUp() == 1)
            {
                // draw corner
                Vector3 NO;
                Vector3 NE;
                Vector3 SO;
                Vector3 SE;
                NO = gridTile.pointUpSO;
                NE = gridTile.pointUpSE;
                SO = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
                SE = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);
                listVert.Add(NE);
                listVert.Add(NO);
                listVert.Add(SE);
                listVert.Add(SO);
                numFace++;
            }
        }
        if(etatE == 1)
        {
            if(gridTile.ProxyTileEst.solidFaceUp() == 1)
            {
                // draw corner
                Vector3 NO;
                Vector3 NE;
                Vector3 SO;
                Vector3 SE;
                NO = gridTile.pointUpNE;
                NE = gridTile.pointUpSE;
                SO = Vector3.Lerp(gridTile.pointDownNE, gridTile.pointUpNE, 0.5f);
                SE = Vector3.Lerp(gridTile.pointDownSE, gridTile.pointUpSE, 0.5f);
                listVert.Add(NO);
                listVert.Add(NE);
                listVert.Add(SO);
                listVert.Add(SE);
                numFace++;
            }
        }
        if(etatO == 1)
        {
            if(gridTile.ProxyTileOuest.solidFaceUp() == 1)
            {
                // draw corner
                Vector3 NO;
                Vector3 NE;
                Vector3 SO;
                Vector3 SE;
                NO = gridTile.pointUpNO;
                NE = gridTile.pointUpSO;
                SO = Vector3.Lerp(gridTile.pointDownNO, gridTile.pointUpNO, 0.5f);
                SE = Vector3.Lerp(gridTile.pointDownSO, gridTile.pointUpSO, 0.5f);
                listVert.Add(NE);
                listVert.Add(NO);
                listVert.Add(SE);
                listVert.Add(SO);
                numFace++;
            }
        }

        // create Mesh
        meshTerrain = SetMeshPlane(listVert, numFace);

        if(gridTile.face != 0)
        {
            // reverse mesh (tech du chlag)
            meshTerrain.triangles = meshTerrain.triangles.Reverse().ToArray();
        }

        // set uvs
        uvMeshTerrain = new Vector2[meshTerrain.vertices.Length];
        for (int i = 0; i < uvMeshTerrain.Length; i++)
        {
            uvMeshTerrain[i] = new Vector2(meshTerrain.vertices[i].x, meshTerrain.vertices[i].z);
        }

        meshTerrain.uv = uvMeshTerrain;

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
        meshRendererTerrain.sharedMaterial= gridTile.gridTileManager.GetMaterialTile();
    }

    public void drawMeshLiquid()
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

    public Mesh SetMeshPlane(List<Vector3> listVert, int numFaceToDraw)
    {
        Mesh meshRetour = new Mesh();
        meshRetour.Clear();
        // vert
        Vector3[] vert = new Vector3[4 * numFaceToDraw];
        for (int i = 0; i < numFaceToDraw*4; i=i+4)
        {
            vert[i+0] = transform.InverseTransformPoint(listVert[i+0]);
            vert[i+1] = transform.InverseTransformPoint(listVert[i+1]);
            vert[i+2] = transform.InverseTransformPoint(listVert[i+2]);
            vert[i+3] = transform.InverseTransformPoint(listVert[i+3]);
        }
        meshRetour.vertices = vert;
        // triangle
        int[] triangles = new int[6 * numFaceToDraw];
        int y = 0;
        for (int i = 0; i < numFaceToDraw*6; i=i+6)
        {
            triangles[i+0] = 2+y;
            triangles[i+1] = 1+y;
            triangles[i+2] = 0+y;
            triangles[i+3] = 2+y;
            triangles[i+4] = 3+y;
            triangles[i+5] = 1+y;
            y = y + 4;
        }
        meshRetour.triangles = triangles;

        // clear
        listVert.Clear();

        return meshRetour;
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
        if(gridTile.ProxyTileUp != null)
        {
            if(gridTile.ProxyTileUp.gridTileManager.tileTerrainType == 1)
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
            if(gridTile.ProxyTileNord.gridTileManager.tileTerrainType == 1 &&
            gridTile.ProxyTileSud.gridTileManager.tileTerrainType == 1 &&
            gridTile.ProxyTileOuest.gridTileManager.tileTerrainType == 1 &&
            gridTile.ProxyTileEst.gridTileManager.tileTerrainType == 1 &&
            gridTile.ProxyTileUp.gridTileManager.tileTerrainType == 1)
            {
                // do sepuku
                DestroyImmediate(gameObject);
            } else{
                DrawMeshSolid();
                ShowMeshTerrain();
            }
        }
        catch
        {
            DrawMeshSolid();
            ShowMeshTerrain();
        }
    }

    void DrawGameObjectLiquid()
    {
        try{
            if(gridTile.ProxyTileUp.gridTileManager.tileTerrainType == 0)
            {
                drawMeshLiquid();
                ShowMeshTerrain();
            } else{
                gameObject.SetActive(false);
            }
        }
        catch
        {
            drawMeshLiquid();
            ShowMeshTerrain();
        }
    }

    void DrawGameObjectAir()
    {
        gameObject.SetActive(false);
    }

    void DrawGameObjectSpace()
    {
        gameObject.SetActive(false);
    }
}
