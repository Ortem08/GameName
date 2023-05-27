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
        AudioClip soundClipSpin = Resources.Load<AudioClip>("spinSound");
        soundClipBottleBreak = Resources.Load<AudioClip>("bottleBreak" + random.Next(1,4));
        Debug.Log(soundClipBottleBreak);
        gameObject.AddComponent<AudioSource>().clip = soundClipSpin;
        //gameObject.GetComponent<AudioSource>().outputAudioMixerGroup = "Master";
        gameObject.GetComponent<AudioSource>().volume = 0.02f;
        gameObject.GetComponent<AudioSource>().loop = true;
        gameObject.GetComponent<AudioSource>().Play();
        vector = (spawnpoint - startPosition).normalized;   
    }

    void Update()
    {
        for (var i = 0; i < 10; i++)
        {
            var pos = gameObject.transform.position;
            gameObject.transform.position = new Vector3((float)(pos.x + 0.001 * vector.x), (float)(pos.y + 0.001 * vector.y), 0);
            if ((pos - spawnpoint).magnitude < 10e-2)
            {
                bottleScript.animationIsDone = true;
                var soundSpeaker = new GameObject();
                soundSpeaker.AddComponent<AudioSource>().clip = soundClipBottleBreak;
                soundSpeaker.GetComponent<AudioSource>().volume = 0.02f;
                soundSpeaker.GetComponent<AudioSource>().Play();
                //Destroy(soundSpeaker);
                Destroy(gameObject);
                break;
            }
        }
    }
}
