using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //vars
    // speed of mouvement
    public float speedBase = 1f;
    // sensivity of camera rotation
    public float sensitivity = 10f;

    // curent grid to rotate around
    public GameObject curentGrid;

    GridTileGameObject oldTile;
    GridTileGameObject selectedTile;

    // Start is called before the first frame update
    void Start()
    {
        //Set Cursor to not be visible
        Cursor.visible = true;
        GridPlanetGeneration gridPlanet = curentGrid.GetComponent<GridPlanetGeneration>();
        gameObject.transform.Translate(gameObject.transform.up * (1 + gridPlanet.planetSettings.radius + gridPlanet.planetSettings.height+2));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCam();
        RayCastHitDetectTile();
    }

    // mouve cam update
    void UpdateCam()
    {
        float speed = speedBase;
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = speed * 2;
        }
        // arrow key (relative to camera not the world)
        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // go right
            transform.RotateAround(curentGrid.transform.position, -transform.forward, speed);
        }
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q))
        {
            // go Left
            transform.RotateAround(curentGrid.transform.position, transform.forward, speed);
        }
        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // go Back
            transform.RotateAround(curentGrid.transform.position, Vector3.Cross(transform.forward, transform.up), speed);
        }
        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z))
        {
            // go Front
            transform.RotateAround(curentGrid.transform.position, -Vector3.Cross(transform.forward, transform.up), speed);
        }
        transform.position = gameObject.transform.position * ((-Input.GetAxis("Mouse ScrollWheel")+1));

        // rotation
        if(Input.GetMouseButton(2))
        {
            // rotate cam
            transform.RotateAround(transform.position, transform.right, Input.GetAxis("Mouse Y"));
            transform.RotateAround(transform.position, transform.position - curentGrid.transform.position, -Input.GetAxis("Mouse X"));
        }
    }

    void RayCastHitDetectTile()
    {
        if(oldTile!=null)
        {
            if(oldTile!=selectedTile)
            {
                oldTile.HideTileLine();
            }
        }
        RaycastHit hit;
        Ray ray;
        // set Layer (3 = Buildable)
        int layerMask = 3;
        // invert (ingore everything except 3)
        layerMask = ~layerMask;
        // raycast to put object
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            oldTile = hit.transform.gameObject.GetComponent<GridTileGameObject>();
            oldTile.ShowTileLine();
            if(Input.GetMouseButton(0))
            {
                if(selectedTile!=null)
                {
                    selectedTile.SetLineColor(selectedTile.gridTile.gridTileManager.GetColorTile());
                    selectedTile.HideTileLine();
                }
                selectedTile = oldTile;
                selectedTile.SetLineColor(Color.black);
                selectedTile.ShowTileLine();
            }
        }
    }
}
