using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HappyCube : MonoBehaviour
{
    protected MeshFilter meshFilter;
    protected Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        mesh.name = "GeneratedMesh";

        mesh.vertices = GenerateVerts();
        mesh.triangles = GenerateTrigs();

        mesh.RecalculateNormals();  
        mesh.RecalculateBounds();

        meshFilter = gameObject.AddComponent<MeshFilter>(); 
        meshFilter.mesh = mesh;

    }

    private int[] GenerateTrigs()
    {
        return new int[] {
            // bot
            1,0,2,
            2,0,3,

            // top 
            4,5,6,
            4,6,7,

            // left
            4,7,0,
            7,3,0,

            // right 
            5,6,1,
            6,2,1,

            // front 
            7,6,2,
            6,2,3,

            // back
            4,5,1,
            5,1,0
        };
    }

    private Vector3[] GenerateVerts()
    {
        return new Vector3[] { 
            // bot
            new Vector3(-1,0,1),
            new Vector3(1,0,1),
            new Vector3(1,0,-1),
            new Vector3(-1,0,-1),

            //top 
            new Vector3(-1,2,1),
            new Vector3(1,2,1),
            new Vector3(1,2,-1),
            new Vector3(-1,2,-1)
        };
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
