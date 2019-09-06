using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puse : MonoBehaviour
{
    public GameObject PausePanel;
    public float timer;
    public bool ispuse;
    public bool guipuse;
   
    public void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispuse == false)
        {
            PausePanel.SetActive(true);
            ispuse = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispuse == true)
        {
            PausePanel.SetActive(false);
            ispuse = false;
        }
        if (ispuse == true)
        {
            timer = 0;
            guipuse = true;

        }
        else if (ispuse == false)
        {
            timer = 1f;
            guipuse = false;

        }
    }


   
}
