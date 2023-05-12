using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayPressed()
    {
        SceneManager.LoadScene("GameTrial");
    }
    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("exit pressed");
    }
}

