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

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(Input.GetKey(KeyCode.RightArrow))
        {
            // go right
            transform.Translate(new Vector3(speed,0,0));
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            // go Left
            transform.Translate(new Vector3(-speed,0,0));
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            // go Back
            transform.Translate(new Vector3(0,0,-speed));
        }
        if(Input.GetKey(KeyCode.UpArrow))
        {
            // go Front
            transform.Translate(new Vector3(0,0,speed));
        }

        // rotation
        if(Input.GetMouseButton(2))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X")* sensitivity, 0);
            transform.Rotate(-Input.GetAxis("Mouse Y")* sensitivity, 0, 0);
        }
    }
}
