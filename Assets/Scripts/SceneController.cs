using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public void LoadScene(int x)
    {
        SceneManager.LoadScene(x);
        }
}