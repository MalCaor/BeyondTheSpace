using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIBeyondTheSpace
{
    public class HideAllConstrucOnglet : MonoBehaviour
    {
        // hide all construc onglet
        public void HideAll()
        {
            foreach (GameObject g in GameObject.FindGameObjectsWithTag("ConstructionOnglet"))
            {
                g.SetActive(false);
            }
        }
    }

}
