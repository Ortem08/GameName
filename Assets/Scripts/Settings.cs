using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer AM;
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

    public void SetQuality(int q)
    {
        QualitySettings.SetQualityLevel(q);
    }
}
