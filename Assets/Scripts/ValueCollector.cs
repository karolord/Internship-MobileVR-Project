using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ValueCollector : MonoBehaviour
{
    public float value = 0.0f;
    public float Increment;
    [SerializeField]
    private TMPro.TextMeshProUGUI _textValue;

    // void Update(){
    //    Add();
    // }
   public void Add(){
         value += Increment;
         Debug.Log(value);
         _textValue.text = value.ToString();
   }
   public void Subtract(){
    if(value > 0){
         value -= Increment;
        _textValue.text = value.ToString();
    }
    if(value <= 0){
        value = 0;
        _textValue.text = value.ToString();
    }
   }
}
