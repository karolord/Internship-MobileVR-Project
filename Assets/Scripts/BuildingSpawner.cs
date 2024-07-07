using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBuilding(GameObject prefab)
    {
        if(prefab == null)
        {
            Debug.LogError("Prefab is null");
            return;
        }
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        GameObject Building = Instantiate(prefab, transform.position, Quaternion.identity);
        Building.transform.SetParent(transform);

    }
}
