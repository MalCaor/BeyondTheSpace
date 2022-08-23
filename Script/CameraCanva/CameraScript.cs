using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //vars
    // speed of mouvement
    public float speed = 1f;
    // sensivity of camera rotation
    public float sensitivity = 10f;

    // curent grid to rotate around
    public GameObject curentGrid;

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
    }

    // mouve cam update
    void UpdateCam()
    {
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
            transform.Rotate(0, Input.GetAxis("Mouse X")* sensitivity, 0);
            transform.Rotate(-Input.GetAxis("Mouse Y")* sensitivity, 0, 0);
        }
    }
}
