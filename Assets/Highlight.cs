using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Highlight : MonoBehaviour
{


    public float _timer = 0f;
    private bool _Gazing;

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
            _timer += Time.deltaTime;
            if (_timer >= 2f)
            {
                AddHighlight();
                _timer = 0f;
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
