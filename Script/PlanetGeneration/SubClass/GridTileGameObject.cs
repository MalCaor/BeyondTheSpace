using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTileGameObject : MonoBehaviour
{
    // public var
    public GridTile gridTile;
    // Proxy GridTile
    public GameObject ProxyTileNord;
    public GameObject ProxyTileSud;
    public GameObject ProxyTileOuest;
    public GameObject ProxyTileEst;
    public GameObject ProxyTileUp;
    public GameObject ProxyTileDown;

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
            this.ProxyTileNord = gridTile.ProxyTileNord.tile;
        }
        if(gridTile.ProxyTileSud!=null)
        {
            this.ProxyTileSud = gridTile.ProxyTileSud.tile;
        }
        if(gridTile.ProxyTileOuest!=null)
        {
            this.ProxyTileOuest = gridTile.ProxyTileOuest.tile;
        }
        if(gridTile.ProxyTileEst!=null)
        {
            this.ProxyTileEst = gridTile.ProxyTileEst.tile;
        }
        if(gridTile.ProxyTileUp!=null)
        {
            this.ProxyTileUp = gridTile.ProxyTileUp.tile;
        }
        if(gridTile.ProxyTileDown!=null)
        {
            this.ProxyTileDown = gridTile.ProxyTileDown.tile;
        }
    }
}
