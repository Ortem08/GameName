using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer AM;

    public Toggle FullScreen;
    public Slider Volume;
    public TMP_Dropdown Quality;

    private void Start()
    {
        if (PlayerPrefs.HasKey("FullScreenValue"))
        {
            var fsVal = Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenValue"));
            FullScreen.isOn = fsVal;
            Screen.fullScreen = fsVal;
        }

        if (PlayerPrefs.HasKey("VolumeValue"))
        {
            var volVal = PlayerPrefs.GetFloat("VolumeValue");
            Volume.value = volVal;
            AM.SetFloat("MasterVolume", volVal);
        }
        else
            AM.SetFloat("MasterVolume", -10);

        if (PlayerPrefs.HasKey("QualityValue"))
        {
            var qulVal = PlayerPrefs.GetInt("QualityValue");
            Quality.value = qulVal;
            QualitySettings.SetQualityLevel(qulVal);
        }
    }
        

    //public Dropdown dropdown;

    //private Resolution[] rsl;
    //private List<string> resolutions;

    //private void Start()
    //{
    //    resolutions = new List<string>();
    //    rsl = Screen.resolutions;
    //    foreach (var i in rsl)
    //    {
    //        resolutions.Add(i.width + "x" + i.height);
    //    }
    //    dropdown.ClearOptions();
    //    dropdown.AddOptions(resolutions);
    //}

    public void FullScreenToggle() => Screen.fullScreen = !Screen.fullScreen;

    public void AudioVolume(float sliderValue) => AM.SetFloat("MasterVolume", sliderValue);

    public void SetQuality(int q) => QualitySettings.SetQualityLevel(q);

    public void SaveSettings()
    {
        var intFullScreen = FullScreen.isOn ? 1 : 0;

        PlayerPrefs.SetInt("FullScreenValue", intFullScreen);
        PlayerPrefs.SetFloat("VolumeValue", Volume.value);
        PlayerPrefs.SetInt("QualityValue", Quality.value);
    }
}
