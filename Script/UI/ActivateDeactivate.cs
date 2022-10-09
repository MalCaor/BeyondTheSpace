using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UIBeyondTheSpace
{
    public class ActivateDeactivate : MonoBehaviour
    {
        // switch visiblility
        public void SwitchVisibility()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }

}
