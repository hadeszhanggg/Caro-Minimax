using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Quit_play : MonoBehaviour
{
  public void quitgame()
    {
        Debug.Log("Thoát");
        SceneManager.LoadScene("MainScreen");
        Application.Quit();
    }
}
