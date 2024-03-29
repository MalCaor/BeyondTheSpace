using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// custom Class Using
using GridTileManagerBeyondTheSpace;

namespace GridTileBeyondTheSpace
{
    [System.Serializable]
    public class GridTile
    {
        // name for the player
        public string name;

        // parent gameObject
        public GameObject planet;

        // GameObject in world
        public GameObject tileGameObject;
        public GridTileGameObject gridTileGameObject;

        // tile mana
        public GridTileManager gridTileManager;

        // Proxy GridTile
        public GridTile ProxyTileNord;
        public GridTile ProxyTileSud;
        public GridTile ProxyTileOuest;
        public GridTile ProxyTileEst;
        public GridTile ProxyTileUp;
        public GridTile ProxyTileDown;

        // Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5
        public int face;

        // position in face grid
        // if 0 the tile is the first one, if = max then it's on the other side
        [HideInInspector]
        public int Npos;
        [HideInInspector]
        public int Opos;
        [HideInInspector]
        public int Epos;
        [HideInInspector]
        public int Spos;
        [HideInInspector]
        public int Upos;
        [HideInInspector]
        public int Dpos;

        // point that delimite the tile in a 3d space
        // points down
        // NO = NordOuest / NE NordEst / SE SudEst / SO SudOuest
        [HideInInspector]
        public Vector3 pointDownNO;
        [HideInInspector]
        public Vector3 pointDownNE;
        [HideInInspector]
        public Vector3 pointDownSO;
        [HideInInspector]
        public Vector3 pointDownSE;
        // points up
        [HideInInspector]
        public Vector3 pointUpNO;
        [HideInInspector]
        public Vector3 pointUpNE;
        [HideInInspector]
        public Vector3 pointUpSO;
        [HideInInspector]
        public Vector3 pointUpSE;

        // constructor
        public GridTile(int numFace, int N, int O, int E, int S, int D, int U)
        {
            // set num Face ( Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5 )
            this.face = numFace;
            // set pos in grid
            this.Npos = N;
            this.Opos = O;
            this.Epos = E;
            this.Spos = S;
            this.Upos = U;
            this.Dpos = D;

            string faceName = "";
            switch (face)
            {
                case 0:
                faceName = "Nord";
                break;
                case 1:
                faceName = "Est";
                break;
                case 2:
                faceName = "Ouest";
                break;
                case 3:
                faceName = "Front";
                break;
                case 4:
                faceName = "Back";
                break;
                case 5:
                faceName = "Sud";
                break;
                default:
                break;
            }

            // set name
            this.name = "Tile Face " + faceName + " : N " + Npos + ", O " + Opos + ", E " + Epos + ", S " + Spos + ", D " + Dpos;
            
            // set tileManager
            this.gridTileManager = new GridTileManager();
        }

        public int solidFaceUp()
        {
            if(this.ProxyTileUp != null)
            {
                if(ProxyTileUp.gridTileManager.tileTerrainType==1)
                {
                    return 1;
                } else {
                    return 0;
                }
            }
            return 0;
        }

        // set square
        public void InitSquare(GameObject planet, Vector3 NO, Vector3 NE, Vector3 SO, Vector3 SE, Vector3 NOH, Vector3 NEH, Vector3 SOH, Vector3 SEH)
        {
            this.planet = planet;

            pointDownNO = NO;
            pointDownNE = NE;
            pointDownSO = SO;
            pointDownSE = SE;

            pointUpNO = NOH;
            pointUpNE = NEH;
            pointUpSO = SOH;
            pointUpSE = SEH;

            
        }

        public void SetProxyTileGrid(List<List<GridTile>> gridGlob)
        {
            // call findTileOtherFace for each Proxy
            ProxyTileNord = findTileOtherFace(gridGlob, 0);
            ProxyTileSud = findTileOtherFace(gridGlob, 2);
            ProxyTileOuest = findTileOtherFace(gridGlob, 3);
            ProxyTileEst = findTileOtherFace(gridGlob, 1);
            ProxyTileUp = findTileOtherFace(gridGlob, 4);
            ProxyTileDown = findTileOtherFace(gridGlob, 5);

            
        }

        /// <summary>
        /// Init the Tile mana
        /// </summary>
        public void SetTileManager()
        {
            bool destructibility = true;
            if (ProxyTileDown == null)
            {
                destructibility = false;
            }
            gridTileManager.SetTileManager(destructibility);
        }

        /// <summary>
        /// Various stuff for init the gameObject
        /// </summary>
        public void InitTileGridGameObject()
        {
            // set the grid
            tileGameObject = new GameObject();
            tileGameObject.transform.parent = planet.transform;

            // set Link to object in GameObject
            GridTileGameObject linkToThis = tileGameObject.AddComponent<GridTileGameObject>();
            linkToThis.gridTile = this;
            gridTileGameObject = linkToThis;

            // set pos to mid
            Vector3 mid = Vector3.Lerp(pointUpSO, pointDownNE, 0.5f);
            tileGameObject.transform.position = mid;
            tileGameObject.name = this.name;

            // set Layer to TileLayer
            tileGameObject.GetComponent<GridTileGameObject>().SetTileLayer(3);
            // set line
            tileGameObject.GetComponent<GridTileGameObject>().InitLineTile();
            // set mesh
            tileGameObject.GetComponent<GridTileGameObject>().InitMeshBoxTile();
            // set mesh Col
            tileGameObject.GetComponent<GridTileGameObject>().InitMeshCollider();
            // set col tile
            tileGameObject.GetComponent<GridTileGameObject>().SetColTileAtri();
            // hide grid
            tileGameObject.GetComponent<GridTileGameObject>().HideMeshTerrain();
        }

        /// <summary>
        /// Find a Tile even on the other Face
        /// dirTile = direction ( Nord 0 / Est 1 / Ouest 2 / Sud 3 / Up 4 / Down 5 )
        /// </summary>
        GridTile findTileOtherFace(List<List<GridTile>> gridGlob, int dirTile)
        {
            // up and down like normal (will retrun null if can't find)
            if(dirTile == 4)
            {
                // Up (4)
                return gridGlob[this.face].Find((x) => x.Npos==this.Npos && x.Opos==this.Opos && x.Dpos==this.Dpos+1);
            }
            if(dirTile == 5)
            {
                // Down (5)
                return gridGlob[this.face].Find((x) => x.Npos==this.Npos && x.Opos==this.Opos && x.Dpos==this.Dpos-1);
            }
            
            // Will need to find Tile on other "column"

            // first num is facenum ( Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5 )
            // second is direction 0 = Up / 1 = Right / 2 = Down / 3 = Left
            int[,] linkFaceToFace = new int[6,4];
            // face Nord
            //       ^ Back
            //     +---+
            //Est< |   | > Ouest
            //     +---+
            //      \/ Front
            linkFaceToFace[0, 0] = 4;
            linkFaceToFace[0, 1] = 2;
            linkFaceToFace[0, 2] = 3;
            linkFaceToFace[0, 3] = 1;
            // face Est
            //         ^ Nord
            //       +---+
            //Front< |   | > Back
            //       +---+
            //        \/ Sud
            linkFaceToFace[1, 0] = 0;
            linkFaceToFace[1, 1] = 4;
            linkFaceToFace[1, 2] = 5;
            linkFaceToFace[1, 3] = 3;
            // face Ouest
            //         ^ Nord
            //       +---+
            // Back< |   | > Front
            //       +---+
            //        \/ Sud
            linkFaceToFace[2, 0] = 0;
            linkFaceToFace[2, 1] = 3;
            linkFaceToFace[2, 2] = 5;
            linkFaceToFace[2, 3] = 4;
            // face Front
            //         ^ Nord
            //       +---+
            //Ouest< |   | > Est
            //       +---+
            //        \/ Sud
            linkFaceToFace[3, 0] = 0;
            linkFaceToFace[3, 1] = 1;
            linkFaceToFace[3, 2] = 5;
            linkFaceToFace[3, 3] = 2;
            // face Back
            //       ^ Nord
            //     +---+
            //Est< |   | > Ouest
            //     +---+
            //      \/ Sud
            linkFaceToFace[4, 0] = 0;
            linkFaceToFace[4, 1] = 2;
            linkFaceToFace[4, 2] = 5;
            linkFaceToFace[4, 3] = 1;
            // face Sud
            //       ^ Back
            //     +---+
            //Est< |   | > Ouest
            //     +---+
            //      \/ Front
            linkFaceToFace[5, 0] = 4;
            linkFaceToFace[5, 1] = 2;
            linkFaceToFace[5, 2] = 3;
            linkFaceToFace[5, 3] = 1;
            
            // now find proxy
            if(dirTile == 0){
                if(Npos == 0){
                    // get the grid where the face is
                    List<GridTile> grid = gridGlob[linkFaceToFace[this.face, dirTile]];
                    return foreignTileCoor(grid, this.face, linkFaceToFace[this.face, dirTile]);
                } else {
                    return gridGlob[face].Find((x) => x.Npos==Npos-1 && x.Opos==Opos && x.Dpos==Dpos);
                }
            }
            if(dirTile == 3){
                if(Opos == 0){
                    // get the grid where the face is
                    List<GridTile> grid = gridGlob[linkFaceToFace[this.face, dirTile]];
                    return foreignTileCoor(grid, this.face, linkFaceToFace[this.face, dirTile]);
                } else {
                    return gridGlob[face].Find((x) => x.Npos==Npos && x.Opos==Opos-1 && x.Dpos==Dpos);
                }
            }
            if(dirTile == 2){
                if(Spos == 0){
                    // get the grid where the face is
                    List<GridTile> grid = gridGlob[linkFaceToFace[this.face, dirTile]];
                    return foreignTileCoor(grid, this.face, linkFaceToFace[this.face, dirTile]);
                } else {
                    return gridGlob[face].Find((x) => x.Opos==Opos && x.Spos==Spos-1 && x.Dpos==Dpos);
                }
            }
            if(dirTile == 1){
                if(Epos == 0){
                    // get the grid where the face is
                    List<GridTile> grid = gridGlob[linkFaceToFace[this.face, dirTile]];
                    return foreignTileCoor(grid, this.face, linkFaceToFace[this.face, dirTile]);
                } else {
                    return gridGlob[face].Find((x) => x.Npos==Npos && x.Epos==Epos-1 && x.Dpos==Dpos);
                }
            }

            // Error
            Debug.Log("Error Proxy Tile Atribution :  Tile " + this.name);
            return null;
        }

        GridTile foreignTileCoor(List<GridTile> grid, int faceA, int faceB)
        {
            int SearchNpos = this.Npos;
            int SearchOpos = this.Opos;
            int SearchEpos = this.Epos;
            int SearchSpos = this.Spos;
            // switch for numface ( Nord 0 / Est 1 / Ouest 2 / Front 3 / Back 4 / Sud 5 )
            switch (faceA)
            {
                case 0:
                // NORD
                switch (faceB)
                {
                    case 0:
                    // NORD
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 1:
                    // EST
                    SearchNpos = this.Opos;
                    SearchOpos = this.Spos;
                    SearchEpos = this.Npos;
                    SearchSpos = this.Epos;
                    break;
                    
                    case 2:
                    // OUEST
                    SearchNpos = this.Epos;
                    SearchOpos = this.Npos;
                    SearchEpos = this.Spos;
                    SearchSpos = this.Opos;
                    break;
                    
                    case 3:
                    // FRONT
                    SearchNpos = this.Spos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Npos;
                    break;
                    
                    case 4:
                    // BACK
                    SearchNpos = this.Npos;
                    SearchOpos = this.Opos;
                    SearchEpos = this.Epos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 5:
                    // SUD
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    default:
                    break;
                }
                break;

                case 1:
                // EST
                switch (faceB)
                {
                    case 0:
                    // NORD
                    SearchNpos = this.Epos;
                    SearchOpos = this.Npos;
                    SearchEpos = this.Spos;
                    SearchSpos = this.Opos;
                    break;
                    
                    case 1:
                    // EST
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 2:
                    // OUEST
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 3:
                    // FRONT
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 4:
                    // BACK
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 5:
                    // SUD
                    SearchNpos = this.Epos;
                    SearchOpos = this.Spos;
                    SearchEpos = this.Npos;
                    SearchSpos = this.Opos;
                    break;
                    
                    default:
                    break;
                }
                break;

                case 2:
                // OUEST
                switch (faceB)
                {
                    case 0:
                    // NORD
                    SearchNpos = this.Opos;
                    SearchOpos = this.Spos;
                    SearchEpos = this.Npos;
                    SearchSpos = this.Epos;
                    break;
                    
                    case 1:
                    // EST
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 2:
                    // OUEST
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 3:
                    // FRONT
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 4:
                    // BACK
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 5:
                    // SUD
                    SearchNpos = this.Opos;
                    SearchOpos = this.Npos;
                    SearchEpos = this.Spos;
                    SearchSpos = this.Epos;
                    break;
                    
                    default:
                    break;
                }
                break;

                case 3:
                // FRONT
                switch (faceB)
                {
                    case 0:
                    // NORD
                    SearchNpos = this.Spos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Npos;
                    break;
                    
                    case 1:
                    // EST
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 2:
                    // OUEST
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 3:
                    // FRONT
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 4:
                    // BACK
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 5:
                    // SUD
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    default:
                    break;
                }
                break;

                case 4:
                // BACK
                switch (faceB)
                {
                    case 0:
                    // NORD
                    SearchNpos = this.Npos;
                    SearchOpos = this.Opos;
                    SearchEpos = this.Epos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 1:
                    // EST
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 2:
                    // OUEST
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 3:
                    // FRONT
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 4:
                    // BACK
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 5:
                    // SUD
                    SearchNpos = this.Spos;
                    SearchOpos = this.Opos;
                    SearchEpos = this.Epos;
                    SearchSpos = this.Npos;
                    break;
                    
                    default:
                    break;
                }
                break;

                case 5:
                // SUD
                switch (faceB)
                {
                    case 0:
                    // NORD
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    case 1:
                    // EST
                    SearchNpos = this.Epos;
                    SearchOpos = this.Spos;
                    SearchEpos = this.Npos;
                    SearchSpos = this.Opos;
                    break;
                    
                    case 2:
                    // OUEST
                    SearchNpos = this.Opos;
                    SearchOpos = this.Npos;
                    SearchEpos = this.Spos;
                    SearchSpos = this.Epos;
                    break;
                    
                    case 3:
                    // FRONT
                    SearchNpos = this.Npos;
                    SearchOpos = this.Epos;
                    SearchEpos = this.Opos;
                    SearchSpos = this.Spos;
                    break;
                    
                    case 4:
                    // BACK
                    SearchNpos = this.Spos;
                    SearchOpos = this.Opos;
                    SearchEpos = this.Epos;
                    SearchSpos = this.Npos;
                    break;
                    
                    case 5:
                    // SUD
                    // imposible
                    Debug.LogError("Wrong foreignTileCoor at tile : " + this.name);
                    break;
                    
                    default:
                    break;
                }
                break;

                default:
                break;
            }

            // return find
            GridTile g = grid.Find((x) => x.Npos==SearchNpos && x.Opos==SearchOpos && x.Epos==SearchEpos && x.Spos==SearchSpos && x.Dpos==this.Dpos);
            if(g == null)
            {
                Debug.Log("Error foreignTileCoor Tile is NULL at tile : " + this.name);
            }
            return g;
        }
    }

}
