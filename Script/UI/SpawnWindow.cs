using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIBeyondTheSpace
{
    public class SpawnWindow : MonoBehaviour
    {
        // Spawn Prefab Object
        public void SpawnPrefabObject(string name)
        {
            // spawn object pref from name
            GameObject pref = Instantiate(Resources.Load<GameObject>(name)) as GameObject;
            // make it invisible
            //pref.SetActive(false);
        }
    }

}
