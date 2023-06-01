using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AmbientSoundScript : MonoBehaviour
{
    private List<AudioClip> ambientSounds;
    private AudioSource source;
    private AudioMixer Mixer { get; set; }
    private int soundsCount;
    private System.Random random;
    
    void Start()
    {
        soundsCount = 180;
        random = new System.Random();
        Mixer = Resources.Load<AudioMixer>("AudioMixer");
        ambientSounds = new List<AudioClip>();
        var nightSoundClip = Resources.Load<AudioClip>("Sounds/night");
        for (var i = 1; i < soundsCount; i++)
        {
            var sickSoundClip = Resources.Load<AudioClip>("Sounds/Ambient/ambientSound (" + i.ToString() + ")");
            ambientSounds.Add(sickSoundClip);
        }

        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
        source.clip = nightSoundClip;
        source.loop = true;
        source.Play();

        StartCoroutine(WaitAndPlayAmbientSound());


    }

   
    void Update()
    {

    }

    private IEnumerator WaitAndPlayAmbientSound()
    {
        yield return new WaitForSeconds(5);
        var clip = ambientSounds[random.Next(0, soundsCount - 1)];
        source.PlayOneShot(clip);

        StartCoroutine(WaitAndPlayAmbientSound());

    }
}
