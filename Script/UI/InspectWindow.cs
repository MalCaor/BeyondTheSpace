/*
TODO : FIX THIS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using TextMeshPro;

namespace UIBeyondTheSpace
{
    public class InspectWindow : MonoBehaviour
    {
        TextMeshProUGUI ObjectName;

        void Start()
        {
            ObjectName = gameObject.transform.Find("TextNameObj").GetComponent<TextMeshProUGUI>();
        }

        // Load the info of an object in the inspect window
        public void InspectObject(string name)
        {
            if(gameObject.activeSelf == false)
            {
                // if inspect window is closed
                gameObject.SetActive(true);
                Start();
            }
            
            ObjectName.text = name;
        }
    }
}


*/