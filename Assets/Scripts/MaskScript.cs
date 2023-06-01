using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class MaskScript : MonoBehaviour
{
    private GameObject[] npcs;
    private Vector3 spawnPoint;
    public Sprite MaskImage;

    private System.Random random;
    private AudioMixer Mixer { get; set; }
    private int soundsCount;

    void Start()
    {
        Mixer = Resources.Load<AudioMixer>("AudioMixer");
        soundsCount = 22;
        var clip = Resources.Load<AudioClip>("Sounds/ScreamSounds/Scream" + random.Next(1, soundsCount));
        var soundSpeaker = new GameObject();
        var screamSound = soundSpeaker.AddComponent<AudioSource>();
        screamSound.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];

        npcs = GameObject.FindGameObjectsWithTag("NPC");
        spawnPoint = GameObject.FindGameObjectWithTag("Player").transform.position;

        foreach (var npc in npcs)
        {
            var distance = Mathf.Abs((npc.transform.position - spawnPoint).magnitude);
            if (distance < 10)
            {
                RunAway(npc);
            }
        }
        screamSound.PlayOneShot(clip);
    }

    private void RunAway(GameObject npc)
    {
        var navAgent = npc.GetComponent<NavMeshAgent>();
        navAgent.destination = ((navAgent.transform.position - spawnPoint) * 3) + spawnPoint;
    }
}