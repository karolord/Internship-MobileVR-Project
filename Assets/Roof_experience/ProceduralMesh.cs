using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using VSCodeEditor;

[RequireComponent(typeof(MeshFilter))]

public class ProceduralMesh : MonoBehaviour
{
    // Start is called before the first frame update
    Mesh mesh;

    LineRenderer ln; 

    int[] triangles;
    public float length;
    public float width; 
    public float height;

    float x_0 = 0;
    float y_0 = 0;
    float z_0 = 0;

    //public float x_0_r = 0;
    //public float y_0_r = 0;
    //public float z_0_r = 0;
    //public float w_0_r = 0;

    public bool changeState;

    public bool showTriangle_1;
    public bool showTriangle_2;
    public bool showTriangle_T3;

    Vector3[] vertices;
    Vector3[] Label_inside_1_h;
    Vector3[] Label_inside_1_w;
    Vector3[] Label_inside_2_h;
    Vector3[] Label_inside_2_w;

    bool populated = false;


    public GameObject labelObject;
    private List<GameObject> labels = new List<GameObject>();

    public GameObject formula_UI;
    public Camera camera;
    

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        //mesh.subMeshCount = 2;
        //Debug.Log("Amount of submesh is :" + mesh.subMeshCount);

        //ln = GetComponent<LineRenderer>();

        //TODO: Continue here, instantiate
        settingUpLabelsChildren();
        Debug.Log(labels[0].transform.childCount);
        Debug.Log(labels[0].transform.GetChild(0).GetChild(0).name);
    }

    // Update is called once per frame
    void Update()
    {
        MakeMeshData();
        //RoofGenerate();

        // State, full model = 0; inside model = 1
        if (changeState)
        {
            
            RoofGenerate(1);
        }
        else
        {
            showTriangle_1 = false;
            showTriangle_2 = false;
            RoofGenerate(0);
        }
        //StartCoroutine(Switchingbetween_FullAndPartial_Mesh());
        
    }

    IEnumerator Switchingbetween_FullAndPartial_Mesh()
    {
        MakeMeshData();
        RoofGenerate(0);

        yield return new WaitForSeconds(3);

        Debug.Log("Switch to inside roof");
        MakeMeshData();
        RoofGenerate(1);
        

    }

    private void CreateMesh()
    {
        vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 0) , new Vector3(1,0,1)};
        triangles = new int[] { 0, 1, 2 , 2 ,1, 3} ;
    }

    private void RoofGenerate(int state) 
    {
        /**
         * Procedurally generate roof base on the state
         * 
         * @param state, is the state of the roof
         *      0 - full roof 
         *      1 - inside roof 
         */
        

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
        


        vertices = new Vector3[] { vert_0, vert_1, vert_2, vert_3, vert_4, vert_5, vert_6, vert_7, vert_8, vert_9, vert_10, vert_11, vert_12, vert_13 };

        Vector3 ln_offset = new Vector3(0, 0, -0.05f);
        Label_inside_1_h = new Vector3[] { vert_4 + ln_offset, vert_11 + ln_offset };
        Label_inside_1_w = new Vector3[] { vert_1 + ln_offset, vert_11 + ln_offset };

        Label_inside_2_h = new Vector3[] {  };


        //Vector3[] R1 = new Vector3[] { vert_1, vert_4 };

        //Vector3[] T3 = new Vector3[] { vert_0, vert_4, vert_1 };
        

        //if (!populated)
        //{
        //    settingUpLabelsChildren();
        //    populated = true;
        //}

        ////Amount of Submesh
        //mesh.subMeshCount = 2;

        if ( state == 0 )
        {
            // whole roof triangle
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

            showTriangle_1 = false;
            showTriangle_2 = false;
            showTriangle_T3 = false;

            for ( int i = 0; i < labels.Count; i++)
            {
                labels[i].SetActive(false);
            }
        }
        else
        {
            for ( int i = 0; i < labels.Count; i++ )
            {
                labels[i].SetActive(true);
            }
            // inside roof triangle 
            //triangles = new int[]
            //{
            //    // Width Plane
            //    1, 7, 4,

            //    // Length Plane
            //    10, 4, 11,
            //    11, 4, 12,
            //    12, 4, 5,
            //    5, 13, 12
            //};
            //mesh.Clear();

            
            int[] tri_1 = new int[]
            {
                // Width Plane
                1, 7, 4,
            };            

            
            int[] tri_2 = new int[]
            {
                // Length Plane
                10, 4, 11,
                11, 4, 12,
                12, 4, 5,
                5, 13, 12
            };

            int[] tri_3 = new int[]
            {
                0,4,1
            };

            // TODO: fix not being able to toggle on triangle 2 after triangle 1 toggled 
            // Issue arrive due to order of and constant update (try fixUpdate?)
            if (showTriangle_2)
            {
                
                triangles = tri_2;

                //makeLabel(labels[0], Label_inside_2_h, height.ToString());
                //makeLabel(labels[1], Label_inside_2_w, width.ToString());

                //labels[0].transform.position = label_tri1_h;
                //labels[1].transform.position = label_tri1_w;

                //labels[0].transform.rotation = new Quaternion(0, 0, 0, 0);
                //labels[1].transform.rotation = new Quaternion(0, 0, 0, 0);

                //if (showTriangle_1 == true)
                //{
                //    showTriangle_1 = false;
                //    // TODO: UI here                    
                //}
            }

            if (showTriangle_1)
            {
                
                triangles = tri_1;

                float R1 = Mathf.Sqrt(Mathf.Pow(width / 2, 2) + Mathf.Pow(height, 2));

                Vector3[] R1_length_verts = new Vector3[] { vertices[4], vertices[1] };

                Vector3 label_tri1_h = new Vector3((x_0 - width) / 2 - 0.75f, (y_0 + height) / 2, z_0 + 1.0f / 3.0f * length);
                Vector3 label_tri1_w = new Vector3((vertices[1].x + vertices[11].x) / 2, y_0 - 0.5f, (vertices[1].z + vertices[11].z) / 2 + 0.5f);
                Vector3 label_R1 = new Vector3((vertices[1].x + vertices[4].x) / 2 + 2.25f, (vertices[4].y) / 2, vertices[4].z - 0.5f);

                string R1_text = "R1 = " + R1.ToString();

                makeLabel(labels[0], Label_inside_1_h, height.ToString());
                makeLabel(labels[1], Label_inside_1_w, width.ToString());
                makeLabel(labels[2], R1_length_verts, R1_text);

                labels[0].transform.position = label_tri1_h;
                labels[1].transform.position = label_tri1_w;
                labels[2].transform.position = label_R1;
                

                labels[0].transform.rotation = new Quaternion(0, 0, 0, 0);
                labels[1].transform.rotation = new Quaternion(0, 0, 0, 0);
                labels[2].transform.rotation = new Quaternion(0, 0, 0, 0);


                //TODO: Implement UI like T3

                if (showTriangle_2 == true)
                {
                    showTriangle_2 = false;
                }

                
            }

            if (showTriangle_T3)
            {

                triangles = tri_3;
                float R1 = Mathf.Sqrt(Mathf.Pow(width / 2, 2) + Mathf.Pow(height, 2));
                showT3(R1, vertices);

                formula_UI.transform.position = new Vector3(-6.4f, 6, 3);
                formula_UI.transform.rotation = new Quaternion(0, 1, 0, -1);

                string formula_text = "H<sup>2</sup> = R1<sup>2</sup> + Width<sup>2</sup>";
                string title_text = "Calculating T3";
                showUI(formula_text, title_text);

                //TODO: Make UI look great for all scale
                camera.transform.position = new Vector3(7.5f, 3.5f, 3);
                camera.transform.rotation = new Quaternion(0, 1, 0, -1);
            }
        }    
   }
    private void MakeMeshData()
    {
        mesh.Clear();
        mesh.vertices = vertices;   
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void settingUpLabelsChildren()
    {
        for (int i = 0; i < 3; i++)
        {
            labels.Add(Instantiate(labelObject));
        }
    }

    void makeLabel(GameObject label, Vector3[] label_verts, string label_text)
    {
        LineRenderer ln = label.GetComponent<LineRenderer>();

        ln.SetPositions(label_verts);

        TextMeshProUGUI label_height = label.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        label_height.text = label_text;
    }

    void showT3(float R1_leng, Vector3[] Complete_vertices)
    {
        Vector3[] verts  = Complete_vertices;
        int[] T3_verts = new int[]
        {
            0, 4, 1
        };

        if (verts[0] != null && verts[1] != null)
        {
            float T3_leng = verts[1].z - verts[0].z;
            float T3_H = Mathf.Sqrt(Mathf.Pow(R1_leng, 2) + Mathf.Pow(T3_leng, 2));

            Vector3[] T3_length_verts = new Vector3[] { verts[0], verts[1] };
            Vector3[] T3_height_verts = new Vector3[] { verts[1], verts[4] };
            Vector3[] T3_H_verts = new Vector3[] { verts[0], verts[4] };

            Vector3 label_T3_h = new Vector3((verts[0].x + verts[4].x) / 2 + 1.25f, (verts[4].y) / 2, verts[4].z - 0.5f);
            Vector3 label_T3_w = new Vector3(.25f, 0, (verts[0].z + verts[4].z) / 2 + 1.25f);
            Vector3 label_T3_H = new Vector3((verts[0].x + verts[4].x) / 2 + 1.25f, (verts[4].y) / 2, (verts[0].z + verts[4].z) / 2 - 1.25f);

            string T3_leng_text = "width = " + T3_leng.ToString(); 
            string R1_leng_text = "R1 = " + R1_leng.ToString();
            string T3_H_leng_text = "H = " + T3_leng.ToString();

            makeLabel(labels[0], T3_height_verts, R1_leng_text);
            makeLabel(labels[1], T3_length_verts, T3_leng_text);
            makeLabel(labels[2], T3_H_verts, T3_H_leng_text);

            //TODO get label position of T3
            labels[0].transform.position = label_T3_h;
            labels[1].transform.position = label_T3_w;
            labels[2].transform.position = label_T3_H;

            labels[0].transform.rotation = new Quaternion(0, 1, 0, -1);
            labels[1].transform.rotation = new Quaternion(0, 1, 0, -1);
            labels[2].transform.rotation = new Quaternion(0, 1, 0, -1);

            

        }
        else
        {
            Debug.Log("vert[0], vert[1] doesn't exist");
        }
    }

    void showUI(string formula_text, string title_text)
    {
        TextMeshProUGUI formula = formula_UI.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); 
        TextMeshProUGUI title = formula_UI.transform.GetChild(1).GetComponent<TextMeshProUGUI>(); 

        formula.text = formula_text;
        title.text = title_text;
    }
}
