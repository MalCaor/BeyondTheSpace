using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickBox : MonoBehaviour
{
    // when the mouse click the object
    void OnMouseDown()
    {
        GameObject.Find("CameraGroundView/CanvasGroundView/PanelInspectWindow").GetComponent<InspectWindow>().InspectObject(gameObject.name);
    }
}
