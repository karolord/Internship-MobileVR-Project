using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ValueCollector : MonoBehaviour
{
    public float value = 0.0f;
    [SerializeField]
    private TMPro.TextMeshProUGUI _textValue;

    // void Update(){
    //    Add();
    // }
   public void Add(){
         value += 0.03f;
         Debug.Log(value);
         _textValue.text = value.ToString();
   }
   public void Subtract(){
    if(value > 0){
         value -= 1.0f;
        _textValue.text = value.ToString();
    }
   }
}
