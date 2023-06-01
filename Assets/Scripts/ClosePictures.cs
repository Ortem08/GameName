using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClosePictures : MonoBehaviour
{
    private static bool PictureIsOpen;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    //public void Resume()
    //{
    //    Picture.SetActive(false);
    //    Time.timeScale = 1f;
    //    PictureIsOpen = false;
    //}

    void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 0f;
        PictureIsOpen = false;
    }
}
