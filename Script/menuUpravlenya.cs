using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuUpravlenya : MonoBehaviour
{

    public void LoadScene (int Level)
    {
        SceneManager.LoadScene(Level);
        Debug.Log(Level);
    }
    public void Exit ( )
    {
       Application.Quit();
    }

   
}
