using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class BottleAnimationScript : MonoBehaviour
{
    public Vector3 Spawnpoint;
    public BottleScript BottleScript;

    private Vector3 startPosition;
    private Vector3 vector;
    private AudioClip soundClipBottleBreak;

    private bool OncePlayed { get; set; }
    private AudioMixer Mixer { get; set; }

    void Start()
    {
        startPosition = transform.position;
        soundClipBottleBreak = Resources.Load<AudioClip>("Sounds/bottleBreak" + Random.Range(1, 4));

        var audioSource = gameObject.AddComponent<AudioSource>();
        Mixer = Resources.Load<AudioMixer>("AudioMixer");
        audioSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
        audioSource.clip = Resources.Load<AudioClip>("Sounds/spinSound");
        audioSource.loop = true;
        //audioSource.volume = 0.02f;
        audioSource.Play();
        StartCoroutine(MoveBottle());
        
        vector = (Spawnpoint - startPosition).normalized;
    }

    private IEnumerator MoveBottle()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);

            var pos = transform.position;
            transform.position = new Vector3(pos.x + 0.1f * vector.x, pos.y + 0.1f * vector.y, 0);

            if (!OncePlayed && (pos - Spawnpoint).magnitude < 10e-2)
            {
                OncePlayed = true;
                BottleScript.IsAnimationFinished = true;
                var soundSpeaker = new GameObject();
                var breakSoundSource = soundSpeaker.AddComponent<AudioSource>();
                breakSoundSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
                //breakSoundSource.volume = 0.02f;
                breakSoundSource.PlayOneShot(soundClipBottleBreak);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
