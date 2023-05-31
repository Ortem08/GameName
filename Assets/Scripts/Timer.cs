using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI TimerText;
    public float TargetTime;

    private float gameTime;

    private void Update()
    {
        var minutes = ((int)(TargetTime / 60)).ToString();
        var seconds = TargetTime % 60 < 10 ? "0" + TargetTime % 60 : (TargetTime % 60).ToString();
        TimerText.text =  minutes + ':' + seconds;
        gameTime += 1 * Time.deltaTime;
        if (gameTime >= 1)
        {
            TargetTime -= 1;
            gameTime = 0;
        }
        if (TargetTime < 10)
            TimerText.color = Color.yellow;
        if (TargetTime <= 3)
            TimerText.color = Color.red;
        if(TargetTime < 0.01)
            SceneManager.LoadScene("Menu");
    }
}