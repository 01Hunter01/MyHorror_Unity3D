using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_GUI : MonoBehaviour
{
    
   
    public Light flashlight;
    private bool _isOn = false;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _isOn = !_isOn;
            flashlight.enabled = _isOn;
        }
    }

}
