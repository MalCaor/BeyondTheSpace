using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerConter : MonoBehaviour
{
    // vars
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = string.Format("{0:N2}", Time.time);
    }
}
