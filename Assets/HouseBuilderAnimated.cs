using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HouseBuilderAnimated : MonoBehaviour
{
    public float PlateLength;
    public float MainPlateLength = 100f;
    public float WallHeight;
    public float Spacing;
    public float NogginsSpacing;
    private int _StudCount;
    private int _MainStudCount;
    private int _NogginCount;
    public GameObject PlatePrefab;
    public GameObject StudPrefab;
    public GameObject NogginsPrefab;
    public TMPro.TextMeshProUGUI _Name;
    public TMPro.TextMeshProUGUI _value;

    void Start()
    {
        // MainPlateLength = 8f;
        // PlateLength = 10f;
        // WallHeight = 5f;
        // Spacing = 4f;
        // NogginsSpacing = 2f;
        WallBuilder();
    }
    void AnimationBuilder(GameObject piece, Vector3 start, Vector3 end, Vector3 StartRotation, Vector3 EndRotation, float duration)
    {
        Animation animation = piece.AddComponent<Animation>();
        AnimationClip clip = new AnimationClip();
        clip.legacy = true;
        clip.name = "HouseBuilderAnimation";
        AnimationCurve curve = AnimationCurve.Linear(0, start.x, duration, end.x);
        clip.SetCurve("", typeof(Transform), "localPosition.x", curve);
        curve = AnimationCurve.Linear(0, start.y, duration, end.y);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);
        curve = AnimationCurve.Linear(0, start.z, duration, end.z);
        clip.SetCurve("", typeof(Transform), "localPosition.z", curve);
        curve = AnimationCurve.Linear(0, StartRotation.x, duration, EndRotation.x);
        clip.SetCurve("", typeof(Transform), "localRotation.x", curve);
        curve = AnimationCurve.EaseInOut(0, 0, duration, 90);
        clip.SetCurve("", typeof(Transform), "localRotation.y", curve);
        curve = AnimationCurve.Linear(0, StartRotation.z, duration, EndRotation.z);
        clip.SetCurve("", typeof(Transform), "localRotation.z", curve);
        animation.AddClip(clip, clip.name);
        animation.Play(clip.name);
    }
    public void WallBuilder()
    {
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
    }
    void BottomPlates(){
        StartCoroutine(PlaceBottomPlates(0, 0, 0, 1,0));
        StartCoroutine(PlaceBottomPlates(0, 0, 0, -1,0));
        StartCoroutine(PlaceBottomPlates(0, 0, 0, 1,90));
        StartCoroutine(PlaceBottomPlates(0, 0, 0, -1,90));
    }
    void TopPlates(){
        StartCoroutine(PlaceTopPlates(15, 0, 0, 1,0));
        StartCoroutine(PlaceTopPlates(15, 0, 0, -1,0));
        StartCoroutine(PlaceTopPlates(15, 0, 0, 1,90));
        StartCoroutine(PlaceTopPlates(15, 0, 0, -1,90));
    }

    IEnumerator PlaceBottomPlates(int countdown, float x, float y, float z,float Yrot)
    {
        yield return new WaitForSeconds(countdown);
        GameObject BottomPlate = Instantiate(PlatePrefab);
        BottomPlate.transform.parent = this.transform;
        if(Yrot==0){
        BottomPlate.transform.localScale = new Vector3(PlateLength, .1f, 0.1f);
        BottomPlate.transform.position = new Vector3(x, y, z*MainPlateLength / 2);
        }else{
        BottomPlate.transform.localScale = new Vector3(.1f, .1f, MainPlateLength);
        BottomPlate.transform.position = new Vector3(z*PlateLength / 2, y, x);
        }
        BottomPlate.transform.rotation = Quaternion.Euler(0, Yrot, 0);
        BottomPlate.GetComponent<Highlight>()._name = "BottomPlate";
        AnimationBuilder(BottomPlate, new Vector3(20, 0, 13), BottomPlate.transform.position, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 5);
    }
    IEnumerator PlaceTopPlates(int countdown, float x, float y, float z, float Yrot)
    {
        yield return new WaitForSeconds(countdown);
        GameObject TopPlate = Instantiate(PlatePrefab);
        TopPlate.transform.parent = this.transform;
        if(Yrot==0){
        TopPlate.transform.localScale = new Vector3(PlateLength, .1f, 0.1f);
        TopPlate.transform.position = new Vector3(x, WallHeight, z*MainPlateLength / 2);
        }else{
        TopPlate.transform.localScale = new Vector3(.1f, .1f, MainPlateLength);
        TopPlate.transform.position = new Vector3(z*PlateLength / 2, WallHeight, x);
        }
        TopPlate.GetComponent<Highlight>()._name = "TopPlate";
        AnimationBuilder(TopPlate, new Vector3(20, 0, 13), TopPlate.transform.position, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 5);
    }

    void Studs()
    {
        StartCoroutine(PlaceStuds(5, 0, 0, 1,0));
        StartCoroutine(PlaceStuds(5, 0, 0, -1,0));
        StartCoroutine(PlaceStuds(5, 0, 0, 1,90));
        StartCoroutine(PlaceStuds(5, 0, 0, -1,90));
    }


    IEnumerator PlaceStuds(int countdown, float x, float y, float z,float Yrot)
    {
        yield return new WaitForSeconds(countdown);
        int a;
        if(Yrot==0){
            a = _StudCount;
        }else{
            a = _MainStudCount;
        }
        for (int i = 0; i < a - 1; i++)
        {
            GameObject Stud = Instantiate(StudPrefab);
            Stud.transform.parent = this.transform;
            Stud.transform.localScale = new Vector3(0.1f, WallHeight, 0.1f);
            if(Yrot==0){
            Stud.transform.position = new Vector3(i * Spacing - PlateLength / 2, WallHeight / 2, z*MainPlateLength / 2);
            }else{
            Stud.transform.position = new Vector3(z*PlateLength / 2, WallHeight / 2, i * Spacing - MainPlateLength / 2);
            }
            Stud.transform.rotation = Quaternion.Euler(0, Yrot, 0);
            Stud.GetComponent<Highlight>()._name = "Stud";
            AnimationBuilder(Stud, new Vector3(20, 0, 13), Stud.transform.position, new Vector3(0, -90, 0), new Vector3(0, 0, 0), 5);
        }
        GameObject LastStud = Instantiate(StudPrefab);
        LastStud.transform.parent = this.transform;
        LastStud.transform.localScale = new Vector3(0.1f, WallHeight, 0.1f);
        if(Yrot==0)
        LastStud.transform.position = new Vector3(PlateLength / 2, WallHeight / 2, z*MainPlateLength / 2);
        else
        LastStud.transform.position = new Vector3(z*PlateLength / 2, WallHeight / 2, MainPlateLength / 2);
        LastStud.transform.rotation = Quaternion.Euler(0, Yrot, 0);
        LastStud.GetComponent<Highlight>()._name = "Stud";
        AnimationBuilder(LastStud, new Vector3(20, 0, 13), LastStud.transform.position, new Vector3(0, -90, 0), new Vector3(0, 0, 0), 5);
    }

    
    void Noggins()
    {
        StartCoroutine(PlaceNoggins(10, 0, 0, -1,0));
        StartCoroutine(PlaceNoggins(10, 0, 0, 1,0));
        StartCoroutine(PlaceNoggins(10, 1, 0, 0,90));
        StartCoroutine(PlaceNoggins(10, -1, 0, 0,90));
    }
    IEnumerator PlaceNoggins(int countdown, float x, float y, float z, float Yrot)
    {
        yield return new WaitForSeconds(countdown);
        for (int i = 1; i < _NogginCount - 1; i++)
        {
            GameObject Noggins = Instantiate(NogginsPrefab);
            Noggins.transform.parent = this.transform;
            if(Yrot==0){
            Noggins.transform.localScale = new Vector3(PlateLength, 0.1f, 0.1f);
            Noggins.transform.position = new Vector3(0, WallHeight - i * NogginsSpacing, z*MainPlateLength / 2);
            }else{
            Noggins.transform.localScale = new Vector3(.1f, 0.1f, MainPlateLength);
            Noggins.transform.position = new Vector3(x*PlateLength / 2, WallHeight - i * NogginsSpacing, 0);
            }
            Noggins.GetComponent<Highlight>()._name = "Noggins";
            AnimationBuilder(Noggins, new Vector3(20, 0, 13), Noggins.transform.position, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 5);
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

    public void UpdateMetadata(string name)
    {
        _Name.text = name;
        if (name == "TopPlate" || name == "BottomPlate")
        {
            _value.text = PlateLength.ToString() + "Mm";
        }
        else if (name == "Stud")
        {
            _value.text = WallHeight.ToString() + "Mm";
        }
        else if (name == "Noggins")
        {
            _value.text = PlateLength.ToString() + "Mm";
        }

    }
    IEnumerator transitionTime()
    {
        yield return new WaitForSeconds(5);
    }
}
