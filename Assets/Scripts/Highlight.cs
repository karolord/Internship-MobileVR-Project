using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Highlight : MonoBehaviour
{

    public string _name;

    public float _timer = 0f;
    private bool _Gazing;
    public float Measurement;

    private float blueMultiply = 3.50f;
    private float redGreenMultiply = 0.50f;
 
    private Color originalColor;
    
 
    void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material.color;
    }
    void Update()
    {
        if (_Gazing)
        {
            if(Measurement == 0){
                Measurement = MathF.Max(MathF.Max(gameObject.transform.localScale.x, gameObject.transform.localScale.y), gameObject.transform.localScale.z);
                if(!_name.Contains("Length"))
                _name = "Name:  "+_name + "\n  Length: " + Measurement.ToString() + " Meters";
            }

            _timer += Time.deltaTime;
            if (_timer >= 1f)
            {
                AddHighlight();
                _timer = 0f;
                TextMeshPro text = GameObject.Find("HighlightText").GetComponent<TextMeshPro>();
                if (_name.Length > 50){
                    text.fontSize = 15;
                }else{
                    text.fontSize = 24;
                }
                if(text != null)
                    text.text = _name;
            }

        }
    }
 // OnPointerEnter is called when the pointer enters the GameObject
    public void OnPointerEnter()
    {
        GazedAt(true);
    }

    // OnPointerExit is called when the pointer exits the GameObject
    public void OnPointerExit()
    {
        GazedAt(false);
    }

    void  AddHighlight()
    {
        Debug.Log("Highlighting");
        float red = originalColor.r * redGreenMultiply;
        float blue = originalColor.b * blueMultiply;
        float green = originalColor.g * redGreenMultiply;
 
        gameObject.GetComponent<Renderer>().material.color = new Color(red, green, blue);
    }
    
    void RemoveHighlight()
    {
        gameObject.GetComponent<Renderer>().material.color = originalColor;
    }
    public void GazedAt(bool gazing)
    {
        _Gazing = gazing;
        if (!gazing)
        {
            RemoveHighlight();
            _timer = 0f;
        }
       
    }

 
}
