using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BottleAnimationScript : MonoBehaviour
{

    public Vector3 spawnpoint;
    public BottleScript bottleScript;
    private Vector3 startPosition;
    private Vector3 vector;
    private System.Random random;
    private AudioClip soundClipBottleBreak;



    void Start()
    {
        var random = new System.Random();
        startPosition = transform.position;
        var soundClipSpin = Resources.Load<AudioClip>("spinSound");
        soundClipBottleBreak = Resources.Load<AudioClip>("bottleBreak" + random.Next(1,4));
        var audioSource = gameObject.AddComponent<AudioSource>();
        //gameObject.GetComponent<AudioSource>().outputAudioMixerGroup = "Master";
        audioSource.clip = soundClipSpin;
        audioSource.volume = 0.02f;
        audioSource.loop = true;
        audioSource.Play();
        vector = (spawnpoint - startPosition).normalized;   
    }

    void Update()
    {
        for (var i = 0; i < 10; i++)
        {
            var pos = transform.position;
            transform.position = new Vector3(pos.x + 0.001f * vector.x, pos.y + 0.001f * vector.y, 0);
            if ((pos - spawnpoint).magnitude < 10e-2)
            {
                bottleScript.animationIsDone = true;
                var soundSpeaker = new GameObject();
                var breakSource = soundSpeaker.AddComponent<AudioSource>();
                breakSource.clip = soundClipBottleBreak;
                breakSource.volume = 0.02f;
                breakSource.Play();
                //Destroy(soundSpeaker);
                Destroy(gameObject);
                break;
            }
        }
    }
}
