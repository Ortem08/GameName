using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepSoundScript : MonoBehaviour
{
    public AudioClip[] Steps;

    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        //Source.volume = 0.05f;
    }
    
    public void PlayStep()
    {
        source.PlayOneShot(Steps[Random.Range(0, Steps.Length)]);
    }
}
