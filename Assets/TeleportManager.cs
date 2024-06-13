using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject[] Points;
    // Start is called before the first frame update
    public void Activate(){
        foreach(GameObject point in Points){
            point.SetActive(true);
        }
    }
}
