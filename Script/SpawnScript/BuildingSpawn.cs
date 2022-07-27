using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawn : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // set Layer (3 = Buildable)
        int layerMask = 3;
        // invert (ingore everything except 3)
        layerMask = ~layerMask;
        // raycast to put object
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            transform.position = hit.point + GetComponent<Renderer>().bounds.extents;
        }
        // Build or not
        // rotation
        if(Input.GetMouseButton(0))
        {
            // the building is set up
            gameObject.GetComponent<BuildingSpawn>().enabled = false;
        }
    }
}
