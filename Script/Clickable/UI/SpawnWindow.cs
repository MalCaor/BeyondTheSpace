using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWindow : MonoBehaviour
{
    // switch visiblility
    public void ActivateDeactivate()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
