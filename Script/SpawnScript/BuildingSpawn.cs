using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnScriptBeyondTheSpace
{
    public class BuildingSpawn : MonoBehaviour
    {
        RaycastHit hit;
        Ray ray;
        public int sensitivityRotationPlacement = 50;

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
                // move to point + GetComponent<Renderer>().bounds.extents
                transform.position = hit.point;
                // find vector to center of mass
                Vector3 vectDirection = (transform.position - hit.transform.position).normalized;
                // quat rotation to allign with vect
                Quaternion quatRot = Quaternion.LookRotation(vectDirection);
                // apply rot
                transform.rotation = Quaternion.Slerp(transform.rotation, quatRot, 1);
            }
            // Build or not
            if(Input.GetMouseButton(0))
            {
                // the building is set up
                gameObject.GetComponent<BuildingSpawn>().enabled = false;
            }
        }
    }

}
