using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public void PlayPressed() =>
        SceneManager.LoadScene(PlayerPrefs.GetInt("CompletedLevels") + 1);

    public void ExitPressed() => Application.Quit();
}

