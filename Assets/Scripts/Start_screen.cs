using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Start_screen : MonoBehaviour
{
    private float wait_time = 5.0f;
    void Update()
    {
        if (wait_time <= 0)
        {
            //end wait_time start screen call main screen
            SceneManager.LoadScene("MainScreen");
        }
        else wait_time -= Time.deltaTime;
        
    }
}
