using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ValueCollector : MonoBehaviour
{
    public float value = 0.0f;
    public float mmValue = 0.0f;
    public float Increment;
    public bool Option = true;
    public bool Limiter;
    [SerializeField]
    private TMPro.TextMeshProUGUI _textValue;

    // void Update(){
    //    Add();
    // }
    public void Add()
    {
        if (Option)
        {
            value += Increment;
            Debug.Log(value);
            if (Limiter)
            {
                if (value > 1)
                {
                    value = 1;
                }
            }
            _textValue.text = value.ToString()+" m";
        }
        else
        {
            mmValue = value*1000;
            mmValue += Increment*100;
            Debug.Log(value);
            if (Limiter)
            {
                if (mmValue > 1000)
                {
                    mmValue = 1000;
                }
            }
            _textValue.text = mmValue.ToString()+" mm";
            value = mmValue/1000;
        }
    }
    public void Subtract()
    {
        if (value > 0)
        {
            if (Option)
            {
                value -= Increment;
                _textValue.text = value.ToString()+" m";
            }
            else
            {
                mmValue = value*1000;
                mmValue -= Increment*1000;
                Debug.Log(value);
                _textValue.text = mmValue.ToString()+" mm";
                value = mmValue/1000;
            }
        }
        if (value <= 0)
        {
            value = 0;
            _textValue.text = value.ToString();
        }
    }
    public void SetOption(bool options)
    {
        Option = options;
        if (Option)
        {
            _textValue.text = value.ToString()+" m";
        }
        else
        {
            mmValue = value*1000;
            _textValue.text = mmValue.ToString()+" mm";
        }
    }
}
