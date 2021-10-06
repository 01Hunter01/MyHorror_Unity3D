using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar_GUI : MonoBehaviour
{
    
    [SerializeField] private int _health = 100;
    public Light flashlight;
    
    GUIStyle _style = new GUIStyle();
    private bool _isOn = false;
    

    private void Start()
    {
        _style.fontSize = 50;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _isOn = !_isOn;
            flashlight.enabled = _isOn;
        }
    }

    private void OnGUI()
    {
        
        GUI.Label(new Rect(10, 10, 200, 50), "HP: " + _health, _style);
        GUI.Box(new Rect(10, 70, 200, 50), "HP: " + _health, _style);
    }
}
