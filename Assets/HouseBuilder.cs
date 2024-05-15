using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HouseBuilder : MonoBehaviour
{
    public float PlateLength;
    public float WallHeight;
    public float Spacing;
    public float NogginsSpacing;
    private int _StudCount;
    private int _NogginCount;
    public GameObject PlatePrefab;
    public GameObject StudPrefab;
    public GameObject NogginsPrefab;
    // Start is called before the first frame update
    // void Start()
    // {
    //     WallBuilder();
    // }

    // Update is called once per frame

    public void WallBuilder()
    {
        ClearWall();
        _StudCount = (int)(PlateLength / Spacing) + 1;
        _NogginCount = (int)(WallHeight / NogginsSpacing) + 1;
        PlacePlates();
        PlaceStuds();
        PlaceNoggins();
        
    }


    void PlacePlates(){
        GameObject TopPlate = Instantiate(PlatePrefab);
        TopPlate.transform.parent = this.transform;
        TopPlate.transform.localScale = new Vector3(PlateLength, .1f, 0.1f);
        TopPlate.transform.position = new Vector3(0, WallHeight, 5);
        GameObject BottomPlate = Instantiate(PlatePrefab);
        BottomPlate.transform.parent = this.transform;
        BottomPlate.transform.localScale = new Vector3(PlateLength, .1f, 0.1f);
        BottomPlate.transform.position = new Vector3(0, 0, 5);
    }
    void PlaceStuds(){
        for (int i = 0; i < _StudCount-1; i++)
        {
            GameObject Stud = Instantiate(StudPrefab);
            Stud.transform.parent = this.transform;
            Stud.transform.localScale = new Vector3(0.1f, WallHeight, 0.1f);
            Stud.transform.position = new Vector3(i * Spacing- PlateLength/2, WallHeight / 2, 5);
        }
        GameObject LastStud = Instantiate(StudPrefab);
        LastStud.transform.parent = this.transform;
        LastStud.transform.localScale = new Vector3(0.1f, WallHeight, 0.1f);
        LastStud.transform.position = new Vector3(PlateLength/2, WallHeight / 2, 5);
    }
    void PlaceNoggins(){
        for (int i = 1; i < _NogginCount-1; i++)
        {
            GameObject Noggins = Instantiate(NogginsPrefab);
            Noggins.transform.parent = this.transform;
            Noggins.transform.localScale = new Vector3(PlateLength, 0.1f, 0.1f);
            Noggins.transform.position = new Vector3(0, WallHeight - i * NogginsSpacing, 5);
        }
    }
    public void ClearWall()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
    public void SetPlateLength(float value)
    {
        PlateLength = value;
    }
}
