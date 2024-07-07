using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FrameRoof : MonoBehaviour
{
    Mesh mesh;
    public GameObject labelObject;
    private List<GameObject> labels = new List<GameObject>();

    int[] triangles;
    public float length;
    public float width;
    public float height;

    public float x_0 = 0;
    public float y_0 = 0;
    public float z_0 = 0;

    Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        // transform.position = new Vector3(x_0, y_0, z_0);
        if(this.transform.parent != null)
        {
            length = this.transform.parent.GetComponent<HBAnimated>().PlateLength;
            width = this.transform.parent.GetComponent<HBAnimated>().MainPlateLength;
        }

        Vector3 vert_0 = new Vector3(x_0, y_0, z_0);
        Vector3 vert_1 = new Vector3(x_0, y_0, z_0 + 1.0f / 3.0f * length);
        Vector3 vert_2 = new Vector3(x_0, y_0, z_0 + 2.0f / 3.0f * length);
        Vector3 vert_3 = new Vector3(x_0, y_0, z_0 + length);

        // mid
        Vector3 vert_4 = new Vector3(x_0 - (width) / 2, y_0 + height, z_0 + 1.0f / 3.0f * length);
        Vector3 vert_5 = new Vector3(x_0 - (width) / 2, y_0 + height, z_0 + 2.0f / 3.0f * length);

        // top 
        Vector3 vert_6 = new Vector3(x_0 - width, y_0, z_0);
        Vector3 vert_7 = new Vector3(x_0 - width, y_0, z_0 + 1.0f / 3.0f * length);
        Vector3 vert_8 = new Vector3(x_0 - width, y_0, z_0 + 2.0f / 3.0f * length);
        Vector3 vert_9 = new Vector3(x_0 - width, y_0, z_0 + length);

        // inside
        Vector3 vert_10 = new Vector3(x_0 - width / 2, y_0, z_0);
        Vector3 vert_11 = new Vector3(x_0 - width / 2, y_0, z_0 + 1.0f / 3.0f * length);
        Vector3 vert_12 = new Vector3(x_0 - width / 2, y_0, z_0 + 2.0f / 3.0f * length);
        Vector3 vert_13 = new Vector3(x_0 - width / 2, y_0, z_0 + length);
        Debug.Log(vert_0);
        vert_0 = Quaternion.AngleAxis(90, Vector3.up) * vert_0;
        Debug.Log(vert_0);
        vert_1 = Quaternion.AngleAxis(90, Vector3.up) * vert_1;
        vert_2 = Quaternion.AngleAxis(90, Vector3.up) * vert_2;
        vert_3 = Quaternion.AngleAxis(90, Vector3.up) * vert_3;
        vert_4 = Quaternion.AngleAxis(90, Vector3.up) * vert_4;
        vert_5 = Quaternion.AngleAxis(90, Vector3.up) * vert_5;
        vert_6 = Quaternion.AngleAxis(90, Vector3.up) * vert_6;
        vert_7 = Quaternion.AngleAxis(90, Vector3.up) * vert_7;
        vert_8 = Quaternion.AngleAxis(90, Vector3.up) * vert_8;
        vert_9 = Quaternion.AngleAxis(90, Vector3.up) * vert_9;
        vert_10 = Quaternion.AngleAxis(90, Vector3.up) * vert_10;
        vert_11 = Quaternion.AngleAxis(90, Vector3.up) * vert_11;
        vert_12 = Quaternion.AngleAxis(90, Vector3.up) * vert_12;
        vert_13 = Quaternion.AngleAxis(90, Vector3.up) * vert_13;
        

        vertices = new Vector3[] { vert_0, vert_1, vert_2, vert_3, vert_4, vert_5, vert_6, vert_7, vert_8, vert_9, vert_10, vert_11, vert_12, vert_13 };

        mesh = GetComponent<MeshFilter>().mesh;

        x_0 = transform.position.x;
        y_0 = transform.position.y;
        z_0 = transform.position.z;
        settingUpLabelsChildren(9);
        showEntireRoof();

    }

    public void showEntireRoof()
    {
        clearlabel();

        // _player.transform.position = new Vector3(vertices[0].x, vertices[0].y + 5, vertices[0].z);
        // homeScreen.transform.position = new Vector3(vertices[0].x, _player.transform.position.y, vertices[0].z + 5);
        triangles = new int[] {
        // bot
        0, 4, 1,
        1, 4, 2,
        2, 4, 5,
        5, 3, 2, 

        // left
        0, 6, 4, 

        // top 
        6, 7, 4,
        4, 7, 5,
        5, 7, 8,
        8, 9, 5, 

        // right
        9, 3, 5,
        };

        Vector3[] leng_top = new Vector3[] { vertices[0], vertices[3] };
        Vector3[] leng_bot = new Vector3[] { vertices[6], vertices[9] };
        Vector3[] wid_left = new Vector3[] { vertices[0], vertices[6] };
        Vector3[] wid_right = new Vector3[] { vertices[3], vertices[9] };
        Vector3[] dia_leftTop = new Vector3[] { vertices[4], vertices[6] };
        Vector3[] dia_leftBot = new Vector3[] { vertices[0], vertices[4] };
        Vector3[] dia_rightTop = new Vector3[] { vertices[5], vertices[9] };
        Vector3[] dia_rightBot = new Vector3[] { vertices[5], vertices[3] };
        Vector3[] middle = new Vector3[] { vertices[4], vertices[5] };

        makeLabel(labels[0], leng_top, "");
        makeLabel(labels[1], leng_bot, "");
        makeLabel(labels[2], wid_left, "");
        makeLabel(labels[3], wid_right, "");
        makeLabel(labels[4], dia_leftTop, "");
        makeLabel(labels[5], dia_leftBot, "");
        makeLabel(labels[6], dia_rightTop, "");
        makeLabel(labels[7], dia_rightBot, "");
        makeLabel(labels[8], middle, "");
        MakeMeshData();
    }
    private void settingUpLabelsChildren(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            labels.Add(Instantiate(labelObject));
            labels[i].transform.SetParent(transform);
            // labels[i].transform.localPosition = Vector3.zero;
        }
    }
    void makeLabel(GameObject label, Vector3[] label_verts, string label_text)
    {
        label.SetActive(true);
        LineRenderer ln = label.GetComponent<LineRenderer>();
       Debug.Log(label_verts[0] + " " + label_verts[1]);
        label_verts[0] = label_verts[0] + new Vector3(x_0,y_0, z_0);
        label_verts[1] = label_verts[1] + new Vector3(x_0, y_0, z_0);
        ln.SetPositions(label_verts);
        // ln.SetPosition()

        TextMeshProUGUI label_height = label.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        label_height.text = label_text;
    }
    private void MakeMeshData()
    {
        //mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    void clearlabel()
    {
        for (int i = 0; i < labels.Count; i++)
        {
            labels[i].SetActive(false);
        }
    }
}
