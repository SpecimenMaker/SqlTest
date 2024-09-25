using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Game : MonoBehaviour
{
    public Text playerDisplay;
    public Text scoreDisplay;

    private void Awake()
    {
        if (!DBmanager.LoggIn)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
        playerDisplay.text = "Player:" + DBmanager.username;
        scoreDisplay.text = "Score:" + DBmanager.score;
    }
    public void CallSaveDate()
    {
        StartCoroutine(SavePlayerData());
    }
    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBmanager.username);
        form.AddField("score", DBmanager.score);
        UnityWebRequest www= UnityWebRequest.Post("http://localhost/sqlconnect/savedata.php", form);
        yield return www.SendWebRequest();
        if (www.downloadHandler.text == "0")
        {
            Debug.Log("Game Saved");
        }
        else
        {
            Debug.Log("Save failed #" + www.error);
        }
        DBmanager.LoginOut();
    }
    public void IncreaseScore()
    {
        DBmanager.score++;
        scoreDisplay.text = "Score:" + DBmanager.score;
    }
    public void Organization()
    {
        CallSaveDate();
        UnityEngine.SceneManagement.SceneManager.LoadScene(4);
    }
    public void Exit()
    {
        CallSaveDate();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
