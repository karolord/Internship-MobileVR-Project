using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HBAnimated : HouseBuilderAnimated
{
    public GameObject Roof;
    // void Start(){
    //     WallBuilders();
    // }
     public void WallBuilders()
    {
        Debug.Log("HBAnimated");
        ClearWall();
        _StudCount = (int)(PlateLength / Spacing) + 1;
        _MainStudCount = (int)(MainPlateLength / Spacing) + 1;
        _NogginCount = (int)(WallHeight / NogginsSpacing) + 1;
        // StartCoroutine(PlaceBottomPlates(0, 0, 0, 0));
        BottomPlates();
        Studs();
        Noggins();
        TopPlates();
        // StartCoroutine(PlaceTopPlates(15, 0, 0, 0));
        // GameObject roofs = Instantiate(Roof, new Vector3(PlateLength/2, WallHeight, MainPlateLength/2), Quaternion.identity);
        StartCoroutine(PlaceRoof());
    }
    IEnumerator PlaceRoof(){
        yield return new WaitForSeconds(22f);
        GameObject roofs = Instantiate(Roof, new Vector3(PlateLength/-2, WallHeight, MainPlateLength/-2), Quaternion.identity);
        roofs.transform.parent = this.transform;
        roofs.GetComponent<ProceduralMesh>().length = PlateLength;
        roofs.GetComponent<ProceduralMesh>().width = MainPlateLength;
        // roofs.GetComponent<ProceduralMesh>().hei
    }
}
