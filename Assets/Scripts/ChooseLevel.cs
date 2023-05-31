using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    public Button Level1;
    public Button Level2;
    public Button Level3;

    private int completedLevels;

    private void Start()
    {
        completedLevels = PlayerPrefs.GetInt("CompletedLevels");
        Level1.interactable = false;
        Level2.interactable = false;
        Level3.interactable = false;

        switch (completedLevels)
        {
            case 1:
                Level1.interactable = true;
                break;
            case 2:
                Level1.interactable = true;
                Level2.interactable = true;
                break;
            case 3:
                Level1.interactable = true;
                Level2.interactable = true;
                Level3.interactable = true;
                break;
        }
    }

    public void LoadLevel(int level) => SceneManager.LoadScene(level);

    public void ResetProgress()
    {
        Level1.interactable = false;
        Level2.interactable = false;
        Level3.interactable = false;
        PlayerPrefs.DeleteKey("CompletedLevels");
    }
}
