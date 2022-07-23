using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickBox : MonoBehaviour
{
    GameObject InspecWindow;
    GameObject Libel;
    void Start()
    {
        InspecWindow = GameObject.Find("Main Camera/Canvas Camera/DecriptionWindow");
        Libel = GameObject.Find("Main Camera/Canvas Camera/DecriptionWindow/Libel");
    }

    // when the mouse click the object
    void OnMouseDown()
    {
        InspecWindow.SetActive(true);
        Libel.GetComponent<TextMeshProUGUI>().text = this.name;
    }
}
