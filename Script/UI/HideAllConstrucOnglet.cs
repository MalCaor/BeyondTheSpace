using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
