using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using VSCodeEditor;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(LineRenderer))]

public class ProceduralMesh : MonoBehaviour
{
    // Start is called before the first frame update
    Mesh mesh;

    LineRenderer ln; 

    int[] triangles;
    public float length;
    public float width; 
    public float height;

    public bool changeState;

    public bool showTriangle_1;
    public bool showTriangle_2;

    Vector3[] vertices;
    Vector3[] Label_inside_1_h;
    Vector3[] Label_inside_1_w;


    public GameObject labelObject;
    private GameObject[] labels;
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        mesh.subMeshCount = 2;
        Debug.Log("Amount of submesh is :" + mesh.subMeshCount);

        ln = GetComponent<LineRenderer>();

        //TODO: Continue here, instantiate
        for (int i = 0; i <4; i++)
        {
            labels[i] = Instantiate(labelObject);
        }
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
        
        float x_0 = 0;
        float y_0 = 0;
        float z_0 = 0;

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


        Vector3 ln_offset = new Vector3(0, 0, 0.01f);
        Label_inside_1_h = new Vector3[] { vert_4 + ln_offset, vert_11 + ln_offset };
        Label_inside_1_w = new Vector3[] { vert_7 + ln_offset, vert_1 + ln_offset };

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
        }
        else
        {
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

            // TODO: fix not being able to toggle on triangle 2 after triangle 1 toggled 
            // Issue arrive due to order of and constant update (try fixUpdate?)
            if (showTriangle_2)
            {
                
                triangles = tri_2;

                if (showTriangle_1 == true)
                {
                    showTriangle_1 = false;
                    // TODO: UI here
                }
            }

            if (showTriangle_1)
            {
                
                triangles = tri_1;
                ln.SetPositions(Label_inside_1_h);
                ln.SetPositions(Label_inside_1_w);

                if (showTriangle_2 == true)
                {
                    showTriangle_2 = false;
                }
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
}
