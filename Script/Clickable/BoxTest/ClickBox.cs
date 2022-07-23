using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickBox : MonoBehaviour
{
    GameObject Libel;
    void Start()
    {
        Libel = GameObject.Find("Main Camera/Canvas Camera/DecriptionWindow/Libel");
    }

    // when the mouse click the object
    void OnMouseDown()
    {
        Libel.GetComponent<TextMeshProUGUI>().text = this.name;
    }
}
