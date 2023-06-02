using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class EnergyScript : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioMixer Mixer { get; set; }
    
    private GameObject Player { get; set; }
    private float StartSpeed { get; set; }
    private bool IsOpenSoundFinished { get; set; }
    private bool IsDrinkSoundStarted { get; set; }
    private bool IsDrinkSoundFinished { get; set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        StartSpeed = Player.GetComponent<NavMeshAgent>().speed;

        Mixer = Resources.Load<AudioMixer>("AudioMixer");
        var energyOpenSound = Resources.Load<AudioClip>("Sounds/OpenSounds/Open" + Random.Range(1, 4));
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
        audioSource.volume = 0.5f;
        audioSource.PlayOneShot(energyOpenSound);
        Player.GetComponent<PlayerControl>().BlockedByAnotherScript = true;
    }

    private void Update()
    {
        Debug.Log(Player.GetComponent<NavMeshAgent>().speed);
        if (!IsOpenSoundFinished && !audioSource.isPlaying)
            IsOpenSoundFinished = true;

        if (!IsDrinkSoundFinished && IsDrinkSoundStarted && !audioSource.isPlaying)
        {
            IsDrinkSoundFinished = true;
            Player.GetComponent<PlayerControl>().BlockedByAnotherScript = false;
            SpeedUp();
            StartCoroutine(SpeedDown());
        }

        if (IsOpenSoundFinished && !IsDrinkSoundStarted)
        {
            IsDrinkSoundStarted = true;
            
            var drinkSound = Resources.Load<AudioClip>("Sounds/DrinkSounds/Drink" + Random.Range(1, 4));
            audioSource.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(drinkSound);
            Player.GetComponent<PlayerControl>().BlockedByAnotherScript = true;
        }
    }

    private void SpeedUp() => Player.GetComponent<NavMeshAgent>().speed = 25f;

    private IEnumerator SpeedDown()
    {
        yield return new WaitForSeconds(3);
        Player.GetComponent<NavMeshAgent>().speed = StartSpeed;
    }
}
