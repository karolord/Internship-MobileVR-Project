using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using TMPro;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    private float h;
    private float H;
    private float R1; 

    float x_0 = 0;
    float y_0 = 0;
    float z_0 = 0;

    public bool changeState;

    public bool showTriangle_R1;
    public bool showTriangle_T3;
    public bool showTriangle_T2; 

    Vector3[] vertices;
    Vector3[] Label_inside_1_h;
    Vector3[] Label_inside_1_w;
    Vector3[] Label_inside_2_h;
    Vector3[] Label_inside_2_w;

    public GameObject labelObject;
    private List<GameObject> labels = new List<GameObject>();

    public GameObject _player;
    public GameObject formula_UI;
    public GameObject homeScreen;
    public GameObject tutorialScreen;

    public GameObject practiceScreen; 
    public GameObject Cust_UI;
    public GameObject questionScreen;
    public GameObject presetScreen; 

    bool firstTime = true;

    public Sprite[] formula_illus;

    public UnityEngine.UI.Button m_nextButton, m_prevButton;
    public int state = 0;
    public int cust_button_state;
    public bool paramChange = true;

    float R1_L;
    float T3_L;
    float T2_H;
    float givenLength;

    public Vector3 playerLoc;
    float[] ans;
    int idxAns;

    int selectPreset; 

    void Start()
    {
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
        playerLoc = new Vector3(0, 0, 5);

        R1_L = Mathf.Sqrt(Mathf.Pow(width / 2, 2) + Mathf.Pow(height, 2));
        givenLength = vertices[1].z - vertices[0].z;
        T3_L = Mathf.Sqrt(Mathf.Pow(R1_L, 2) + Mathf.Pow(givenLength, 2));
        T2_H = Mathf.Sqrt(Mathf.Pow(T3_L, 2) - Mathf.Pow(width / 2, 2));

        mesh = GetComponent<MeshFilter>().mesh;
        
        settingUpLabelsChildren(9);

        x_0 = _player.transform.position.x;
        y_0 = _player.transform.position.y;
        z_0 = _player.transform.position.z+5;

        showTutorialScreen();
        ans = new float[4];
    }

    public void showCalculation(int buttonType)
    {


        targetScreen(formula_UI);

        mesh.Clear();


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

        // Specific measurement 
        R1_L = Mathf.Sqrt(Mathf.Pow(width / 2, 2) + Mathf.Pow(height, 2));
        givenLength = vertices[1].z - vertices[0].z;
        T3_L = Mathf.Sqrt(Mathf.Pow(R1_L, 2) + Mathf.Pow(givenLength, 2));
        T2_H = Mathf.Sqrt(Mathf.Pow(T3_L, 2) - Mathf.Pow(width/2, 2));

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

        int[] tri_4 = new int[]
        {
            0, 10, 4,
            4, 10, 6
        };

        if (buttonType == 1)
        {
            state += 1;
        }
        if (buttonType == 0)
        {
            state -= 1;
        }
        
        if (state > 2)
        {
            state = 0;
        }
        if (state < 0)
        {
            state = 2; 
        }

        if (firstTime == true)
        {
            clearlabel();
            showR1(tri_1);
            firstTime = false;
            state = 0;
        }
        else
        {
            if (state == 0)
            {
                clearlabel();
                showR1(tri_1);
            }

            if (state == 1)
            {
                clearlabel();

                triangles = tri_3;
                H = showT3(R1_L, vertices);

                formula_UI.transform.position = new Vector3(-6.4f, 6, 3);
                formula_UI.transform.rotation = new Quaternion(0, 1, 0, -1);

                string formula_text = "H<sup>2</sup> (sloping length of T3) = R1<sup>2</sup> + Width<sup>2</sup>";
                string title_text = "Calculating T3";
                showUI(formula_text, title_text, formula_illus[1]);

                _player.transform.position = new Vector3(7.5f, 3.5f, 3);
                _player.transform.rotation = new Quaternion(0, 1, 0, -1);
            }

            if (state == 2)
            {
                clearlabel();

                triangles = tri_4;

                showT2(T3_L);

                formula_UI.transform.position = new Vector3(-5, 8.9f, 7.2f);
                formula_UI.transform.rotation = new Quaternion(0, 0, 0, 0);

                string T2_formula = "h <sup>2</sup> (sloping height of T2) = H<sup>2</sup> (sloping length of T3)  - (Width/2)<sup>2</sup>";
                string T2_title = "Calculating T2";
                showUI(T2_formula, T2_title, formula_illus[2]);

                _player.transform.position = new Vector3(-5, 2.8f, -8);
                _player.transform.rotation = new Quaternion(0, 0, 0, 0);
            }

        }
        MakeMeshData();
    }

    public void showEntireRoof()
    {

        targetScreen(homeScreen);

        clearlabel();

        _player.transform.position = new Vector3(vertices[4].x, vertices[4].y + 5f, vertices[4].z - 15f);
        _player.transform.rotation = new Quaternion(0, 0, 0, 0);

        homeScreen.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z + 5);
        

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

        Vector3[] leng_top = new Vector3[] {vertices[0], vertices[3]};
        Vector3[] leng_bot = new Vector3[] {vertices[6], vertices[9]};
        Vector3[] wid_left = new Vector3[] {vertices[0], vertices[6]};
        Vector3[] wid_right = new Vector3[] {vertices[3], vertices[9]};
        Vector3[] dia_leftTop = new Vector3[] {vertices[4], vertices[6]};
        Vector3[] dia_leftBot = new Vector3[] {vertices[0], vertices[4]};
        Vector3[] dia_rightTop = new Vector3[] {vertices[5], vertices[9]};
        Vector3[] dia_rightBot = new Vector3[] {vertices[5], vertices[3]};
        Vector3[] middle = new Vector3[] { vertices[4], vertices[5] };


        makeLabel(labels[0], leng_top, length.ToString());
        makeLabel(labels[1], leng_bot, length.ToString());
        makeLabel(labels[2], wid_left, width.ToString());
        makeLabel(labels[3], wid_right, width.ToString());
        makeLabel(labels[4], dia_leftTop, "");
        makeLabel(labels[5], dia_leftBot, "");
        makeLabel(labels[6], dia_rightTop, "");
        makeLabel(labels[7], dia_rightBot, "");
        makeLabel(labels[8], middle, "");

        float offset = 1f;
        labels[0].transform.position = new Vector3(vertices[0].x + offset, vertices[0].y, (vertices[0].z + vertices[3].z) / 2);
        labels[1].transform.position = new Vector3(vertices[6].x - offset, vertices[6].y, (vertices[6].z + vertices[9].z) / 2);
        labels[2].transform.position = new Vector3((vertices[0].x + vertices[6].x) / 2, vertices[0].y, vertices[0].z - offset);
        labels[3].transform.position = new Vector3((vertices[3].x + vertices[9].x) / 2, vertices[3].y, vertices[3].z + offset);


        Quaternion rotation = Quaternion.Euler(0, -180, 0);
        labels[3].transform.rotation = rotation;
        MakeMeshData();
    }
    private void MakeMeshData()
    {
        mesh.vertices = vertices;   
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    private void settingUpLabelsChildren(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            labels.Add(Instantiate(labelObject));
        }
    }

    void makeLabel(GameObject label, Vector3[] label_verts, string label_text)
    {
        label.SetActive(true);
        LineRenderer ln = label.GetComponent<LineRenderer>();

        ln.SetPositions(label_verts);

        TextMeshProUGUI label_height = label.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        label_height.text = label_text;
    }

    float showT3(float R1_leng, Vector3[] Complete_vertices)
    {
        Vector3[] verts  = Complete_vertices;
        int[] T3_verts = new int[]
        {
            0, 4, 1
        };

        float T3_H = 0; 

        if (verts[0] != null && verts[1] != null)
        {
            float T3_leng = verts[1].z - verts[0].z;
            T3_H = Mathf.Sqrt(Mathf.Pow(R1_leng, 2) + Mathf.Pow(T3_leng, 2));

            Vector3[] T3_length_verts = new Vector3[] { verts[0], verts[1] };
            Vector3[] T3_height_verts = new Vector3[] { verts[1], verts[4] };
            Vector3[] T3_H_verts = new Vector3[] { verts[0], verts[4] };

            Vector3 label_T3_h = new Vector3((verts[0].x + verts[4].x) / 2 + 1.25f, (verts[4].y) / 2, verts[4].z - 0.5f);
            Vector3 label_T3_w = new Vector3(.25f, 0, (verts[0].z + verts[4].z) / 2 + 1.25f);
            Vector3 label_T3_H = new Vector3((verts[0].x + verts[4].x) / 2 + 1.25f, (verts[4].y) / 2, (verts[0].z + verts[4].z) / 2 - 1.25f);

            string T3_leng_text = "width = " + T3_leng.ToString(); 
            string R1_leng_text = "R1 = " + R1_leng.ToString();
            string T3_H_leng_text = "H = " + T3_H.ToString();

            makeLabel(labels[0], T3_height_verts, R1_leng_text);
            makeLabel(labels[1], T3_length_verts, T3_leng_text);
            makeLabel(labels[2], T3_H_verts, T3_H_leng_text);

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

        return T3_H;
    }

    void showT2(float T3_H)
    {
        float T2_h_length = Mathf.Sqrt(Mathf.Pow(T3_H, 2) - Mathf.Pow(width/2, 2));

        Vector3[] T2_H_verts = new Vector3[] { vertices[0], vertices[4] };
        Vector3[] T2_width_verts = new Vector3[] { vertices[0], vertices[10] };
        Vector3[] T3_height_verts = new Vector3[] { vertices[10], vertices[4] };

        Vector3 label_T2_H = new Vector3((vertices[0].x + vertices[4].x) / 2 + 1.25f, (vertices[4].y) / 2 , vertices[0].z - 1.25f);
        Vector3 label_T2_w = new Vector3((vertices[0].x + vertices[10].x) / 2 - 0.5f, vertices[0].y, vertices[0].z - 1.25f);
        Vector3 label_T2_h = new Vector3((vertices[0].x + vertices[10].x) / 2 - 4.25f, (vertices[4].y) / 2, vertices[0].z - 1.25f);

        string T2_H_leng_text = "H = " + T3_H.ToString();
        string T2_width_text = "width = " + (width/2).ToString();
        string T2_h_text = "h = " + T2_h_length.ToString();

        makeLabel(labels[0], T2_H_verts, T2_H_leng_text);
        makeLabel(labels[1], T2_width_verts, T2_width_text);
        makeLabel(labels[2], T3_height_verts, T2_h_text);

        labels[0].transform.position = label_T2_H;
        labels[1].transform.position = label_T2_w;
        labels[2].transform.position = label_T2_h;

        labels[0].transform.rotation = new Quaternion(0, 0, 0, 0);
        labels[1].transform.rotation = new Quaternion(0, 0, 0, 0);
        labels[2].transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void showR1(int[] tri_1)
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


        formula_UI.transform.position = new Vector3(-5f, 8.9f, 12.7f);
        formula_UI.transform.rotation = new Quaternion(0, 0, 0, 0);

        string R1_formula = "R1<sup>2</sup> (sloping length of R1) = Height<sup>2</sup> + (Width/2)<sup>2</sup>";
        string R1_title = "Calculating R1";
        showUI(R1_formula, R1_title, formula_illus[0]);

        _player.transform.position = new Vector3(-5, 3.5f, -3);
        _player.transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    void showUI(string formula_text, string title_text, Sprite illus)
    {
        TextMeshProUGUI formula = formula_UI.transform.GetChild(0).GetComponent<TextMeshProUGUI>(); 
        TextMeshProUGUI title = formula_UI.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        UnityEngine.UI.Image Illus = formula_UI.transform.GetChild(2).GetComponent<UnityEngine.UI.Image>();

        formula.text = formula_text;
        title.text = title_text;
        Illus.sprite = illus;
    }

    void clearlabel()
    {
        for (int i = 0; i < labels.Count; i++)
        {
            labels[i].SetActive(false);
        }
    }

    public void showSelectMode()
    {
        firstTime = true;
        mesh.Clear();
        clearlabel();

        targetScreen(homeScreen);

        //_player.transform.position = Vector3.zero;

        _player.transform.position = playerLoc;  
        _player.transform.rotation = new Quaternion(0,0,0,0);
        homeScreen.transform.position = new Vector3(playerLoc.x, playerLoc.y, playerLoc.z + 4);   
    }

    void showTutorialScreen()
    {
        targetScreen(tutorialScreen);

        _player.transform.position = playerLoc;
        //tutorialScreen.transform.position = new Vector3(0, 0, 4); 
        tutorialScreen.transform.position = new Vector3(playerLoc.x,playerLoc.y, playerLoc.z+4); 
    }

    void targetScreen(GameObject target)
    {
        Vector3 away = new Vector3(0, -100, 0);
        if (target == formula_UI)
        {
            homeScreen.transform.position = away;
            tutorialScreen.transform.position = away;
            Cust_UI.transform.position = away;
            questionScreen.transform.position = away;
            practiceScreen.transform.position = away;
            presetScreen.transform.position = away;
        }

        if (target == tutorialScreen)
        {
            formula_UI.transform.position = away;
            homeScreen.transform.position = away;
            Cust_UI.transform.position = away;
            questionScreen.transform.position = away;
            practiceScreen.transform.position = away;
            presetScreen.transform.position = away;

        }

        if (target == homeScreen)
        {
            formula_UI.transform.position = away;
            tutorialScreen.transform.position = away;
            Cust_UI.transform.position = away;
            questionScreen.transform.position = away;
            practiceScreen.transform.position = away;
            presetScreen.transform.position = away;

        }

        if (target == Cust_UI)
        {
            homeScreen.transform.position = away;
            formula_UI.transform.position = away;
            tutorialScreen.transform.position = away;
            questionScreen.transform.position = away;
            practiceScreen.transform.position = away;
            presetScreen.transform.position = away;

        }

        if (target == questionScreen)
        {
            homeScreen.transform.position = away;
            formula_UI.transform.position = away;
            tutorialScreen.transform.position = away;
            Cust_UI.transform.position = away;
            practiceScreen.transform.position = away;
            presetScreen.transform.position = away;


        }

        if (target == practiceScreen) 
        {
            homeScreen.transform.position = away;
            formula_UI.transform.position = away;
            tutorialScreen.transform.position = away;
            Cust_UI.transform.position = away;
            questionScreen.transform.position = away;
            presetScreen.transform.position = away;

        }
        if (target == presetScreen)
        {
            homeScreen.transform.position = away;
            formula_UI.transform.position = away;
            tutorialScreen.transform.position = away;
            Cust_UI.transform.position = away;
            questionScreen.transform.position = away;
            practiceScreen.transform.position = away;
        }
    }

    public void showCustomisationUI()
    {
        targetScreen(Cust_UI);

        //_player.transform.position = Vector3.zero;
        _player.transform.position = playerLoc;
        Cust_UI.transform.position = new Vector3(playerLoc.x, playerLoc.y, playerLoc.z+4);
    }

    public void selectButton(int button)
    {
        cust_button_state = button;
    }

    /// <summary>
    /// Allow user to specify the measurements of the roof 
    /// </summary>
    /// <param name="button">(int) 0 or 1, 0 is substration, 1 is addition </param>
    /// <param name="measurement">(int) 0, 1 or 2, 0 is length, 1 is width, 2 is height </param>
    public void customisation(int measurement)
    {
        //width = 1; 
        //length = 1;
        //height = 1;
        Cust_UI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = length.ToString();
        Cust_UI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = width.ToString();
        Cust_UI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = height.ToString();

        paramChange = true;

        if (width - 1 > 0 && length - 1 > 0 && height - 1 > 0)
        {
            if (cust_button_state == 0)
            {
                if (measurement == 0)
                {
                    length -= 1f;
                    Cust_UI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = length.ToString();
                }
                if (measurement == 1)
                {
                    width -= 1f;
                    Cust_UI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = width.ToString();
                }
                if (measurement == 2)
                {
                    height -= 1f;
                    Cust_UI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = height.ToString();
                }
            }
        }

        if (cust_button_state == 1)
        {
            if (measurement == 0)
            {
                length += 1f;
                Cust_UI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = length.ToString();

            }
            if (measurement == 1)
            {
                width += 1f;
                Cust_UI.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = width.ToString();

            }
            if (measurement == 2)
            {
                height += 1f;
                Cust_UI.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = height.ToString();

            }
        }
    }

    public void showPracticeScreen()
    {
        targetScreen(practiceScreen);
        //_player.transform.position = Vector3.zero;
        _player.transform.position = playerLoc;
        practiceScreen.transform.position = new Vector3(playerLoc.x, playerLoc.y, playerLoc.z + 4);

    }
    string generateQuestion(string param)
    {
        string q = "What is the size of <b> " + param + "</b> when";

        ////TODO: fix measurement so it doesn't have to be cycled through before the number appear
        //float T3 = 0f;

        if (param == "R1")
        {
            q = "What is the size of <b> sloping length <i>" + param + "</i></b> when" + "<b> width / 2 is " + (width / 2).ToString() + "</b> and " + "<b> height is " + height.ToString() + "</b>";
        }

        if (param == "T3")
        {
            q = "What is the size of <b> sloping length <i>" + param + "</i></b> when" + "<b> given base is " + (length/3).ToString() + " (1/3 of length)</b> and " + "<b> sloping length R1 is " + R1_L.ToString() + "</b>";
        }

        if (param == "T2")
        {
            q = "What is the size of <b> sloping height <i>" + param + "</i></b> when" + "<b> sloping length of T3 is " + T3_L.ToString() + "</b> and " + "<b> width / 2 is " + (width / 2).ToString() + "</b>";

        }

        return q;
    }

    string generateAnsPrompt(string selectionType, float selectionValue)
    {
        string ansPrompt = selectionType + " = " + selectionValue.ToString();
        return ansPrompt;
    }

    void generateAns(string selectType, float correctAns)
    {
        ans[0] = width / 2;
        ans[1] = R1_L;
        ans[2] = T3_L;
        ans[3] = T2_H;

        int randOrd;
        float temp;

        // Random shuffle method
        for (int i = ans.Length-1; i > 0; i--)
        {
                randOrd = UnityEngine.Random.Range(0, i);
                temp = ans[i];
                ans[i] = ans[randOrd];
                ans[randOrd] = temp;

        }

        for (int i = 0; i < ans.Length; i++)
        {
            questionScreen.transform.GetChild(0).GetChild(i + 1).GetChild(1).GetComponent<TextMeshProUGUI>().text = generateAnsPrompt(selectType, ans[i]);
        }

        for (int i = 0; i < ans.Length; i++)
        {
            if (ans[i] == correctAns)
            {
                idxAns = i;
            }
        }
    }

    public void showQuestion()
    {
        targetScreen(questionScreen);
        _player.transform.position = playerLoc;
        _player.transform.rotation = Quaternion.Euler(-10,0,0);
        questionScreen.transform.position = new Vector3(playerLoc.x, playerLoc.y, playerLoc.z + 4);

        for (int i = 0; i < ans.Length; i++)
        {
            Debug.Log("Color Changed");
            questionScreen.transform.GetChild(0).GetChild(i + 1).GetComponent<UnityEngine.UI.Image>().color = Color.white;
        } 

        int qType = UnityEngine.Random.Range(0, 2);

        if (paramChange)
        {
            if (qType == 0)
            {
                questionScreen.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = generateQuestion("R1");
                generateAns("sloping length of R1: ", R1_L);

                paramChange = false;
            }

            if (qType == 1)
            {
                questionScreen.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = generateQuestion("T3");
                generateAns("Sloping length of T3: ", T3_L);

                paramChange = false;
            }

            if (qType == 2)
            {
                questionScreen.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = generateQuestion("T2");
                generateAns("Sloping height of T2: ", T2_H);

                paramChange = false;
            }

        }

        Debug.Log(idxAns);
    }

    //TODO: Make New Button to rotate go to the next question

    public void selectAns(int userAns)
    {
        bool IsCorrect = false;
        if (userAns-1 == idxAns)
        {
            // TODO: There's a mistake here
            questionScreen.transform.GetChild(0).GetChild(userAns).GetComponent<UnityEngine.UI.Image>().color = Color.green;
            IsCorrect = true;
            paramChange = true; 
            // TODO: Make Analytics for different parameter
            AnalyticsResult Result = Analytics.CustomEvent("RoofExperience_correctAnswer");

        }
        else
        {
            questionScreen.transform.GetChild(0).GetChild(userAns).GetComponent<UnityEngine.UI.Image>().color = Color.red;
            AnalyticsResult Result = Analytics.CustomEvent("RoofExperience_wrongAnswer");
        }

        if (!IsCorrect)
        {
            questionScreen.transform.GetChild(0).GetChild(idxAns).GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
    }

    public void showPresetScreen()
    {
        targetScreen(presetScreen);
        _player.transform.position = playerLoc;
        _player.transform.rotation = Quaternion.Euler(-10, 0, 0);
        presetScreen.transform.position = new Vector3(playerLoc.x, playerLoc.y, playerLoc.z + 4);
    }
    public void showPresetCustomisation(int state)
    {
        // TODO: Make Preset Cust Screen
        // TODO: Implement Preset Screen logic 

        

        if (state == 1)
        {
            width = 5; 
            height = 5;
            length = 5;
            selectPreset = 1;
            presetScreen.transform.GetChild(0).GetChild(selectPreset).GetComponent<UnityEngine.UI.Image>().color = Color.green;

        }
        if (state == 2)
        {
            width = 10;
            height = 5;
            length = 8;
            selectPreset = 2;
            presetScreen.transform.GetChild(0).GetChild(selectPreset).GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        if (state==3)
        {
            width= 10;
            height = 5; 
            length = 10;
            selectPreset = 3;
            presetScreen.transform.GetChild(0).GetChild(selectPreset).GetComponent<UnityEngine.UI.Image>().color = Color.green;

        }
        if (state == 4)
        {
            width = 12;
            height = 5;
            length = 10;
            selectPreset = 4;
            presetScreen.transform.GetChild(0).GetChild(selectPreset).GetComponent<UnityEngine.UI.Image>().color = Color.green;

        }

        for (int i = 1; i < 4; i++)
        {
            if (i != selectPreset)
            {
                presetScreen.transform.GetChild(0).GetChild(i).GetComponent<UnityEngine.UI.Image>().color = Color.white;
            }
        }
    }
}
