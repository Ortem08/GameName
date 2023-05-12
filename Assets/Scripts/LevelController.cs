using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    private int sceneIndex;
    private int completedLevels;

    void Start()
    {
        Instance = this;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        completedLevels = PlayerPrefs.GetInt("CompletedLevels");
    }

    public void IsEndGame()
    {
        if (sceneIndex == 4)
            Invoke("LoadMainMenu", 1f);
        else
        {
            if (completedLevels < sceneIndex)
                PlayerPrefs.SetInt("CompletedLevels", sceneIndex);
            Invoke("LoadNextLevel", 1f);
        }
    }

    private void LoadNextLevel() => SceneManager.LoadScene(sceneIndex + 1);
    private void LoadMainMenu() => SceneManager.LoadScene("Menu");
}
