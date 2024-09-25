using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button registerButton;
    public Button loginButton;
    public Button playButton;

    public Text playerDisplay;
    private void Start()
    {
        if (DBmanager.LoggIn)
        {
            playerDisplay.text = "Player:" + DBmanager.username;
        }
        playButton.interactable = DBmanager.LoggIn;
    }
    public void GoTORegister()
    {
        SceneManager.LoadScene(1);
    }
    public void GoToLogin()
    {
        SceneManager.LoadScene(2);
    }
    public void GoToGame()
    {
        SceneManager.LoadScene(3);
    }

}
