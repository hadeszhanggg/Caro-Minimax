using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverWindow : MonoBehaviour
{
    public Text winner;
    public Button restartButton;
    private void Awake()
    {
        restartButton.onClick.AddListener(OnClick);
    }
    public void SetName(string s)
    {
        string name = "Bot";
        if (s == "o")
            name = "Bot";
        else name = "You";
        winner.text = "\nWinner is\t"+ name;
    }
    public void OnClick()
    {
        SceneManager.LoadScene("PlayGround");
    }
}
