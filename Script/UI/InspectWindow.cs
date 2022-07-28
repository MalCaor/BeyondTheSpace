using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        ObjectName.text = name;
    }
}
