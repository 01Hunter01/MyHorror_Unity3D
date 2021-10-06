using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HW2_Task_2 : MonoBehaviour
{
    public Color myColor;

    [SerializeField] private Material _material;

    

    private void Update()
    {
        _material.color = myColor;
    }

    void OnGUI()
    {
        myColor = RGBSlider(new Rect(10, 10, 200, 20), myColor);
    }
    Color RGBSlider(Rect screenRect, Color rgb)
    {
        rgb.r = LabelSlider(screenRect, rgb.r, 0.0f, 1.0f, "Red");
        screenRect.y += 20;
        rgb.g = LabelSlider(screenRect, rgb.g, 0.0f, 1.0f, "Green");
        screenRect.y += 20;
        rgb.b = LabelSlider(screenRect, rgb.b, 0.0f, 1.0f, "Blue");
        screenRect.y += 20;
        rgb.a = LabelSlider(screenRect, rgb.a, 0.0f, 1.0f, "Alpha");
        return rgb;
    }
    float LabelSlider(Rect screenRect, float sliderValue, float sliderMinValue, float sliderMaxValue, string labelText)
    {
        GUI.Label(screenRect, labelText);
        screenRect.x += screenRect.width;
        sliderValue = GUI.HorizontalSlider(screenRect, sliderValue, sliderMinValue,
        sliderMaxValue);
        return sliderValue;
    }
    
}
