using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class TeleportScript : MonoBehaviour
{
    public bool IsRecevingPortal;
    
    private GameObject player;
    private AudioMixer Mixer { get; set; }
    private AudioSource teleportSound;
    private AudioClip teleportSoundClip;
    private GameObject anotherPortal;
    

    void Start()
    {
        IsRecevingPortal = false;
        Mixer = Resources.Load<AudioMixer>("AudioMixer");
        player = GameObject.FindGameObjectWithTag("Player");
        var teleports = GameObject.FindGameObjectsWithTag(gameObject.tag);
        foreach(var teleport in teleports)
        {
            if (teleport.name != gameObject.name)
            {
                anotherPortal = teleport;
                break;
            }
        }

        teleportSoundClip = Resources.Load<AudioClip>("Sounds/Teleport1");
        var soundSpeaker = new GameObject();
        teleportSound = soundSpeaker.AddComponent<AudioSource>();
        teleportSound.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsRecevingPortal && collision.gameObject.tag == "Player")
        {
            player.GetComponent<NavMeshAgent>().ResetPath();
            player.GetComponent<NavMeshAgent>().enabled = false;

            var pos = anotherPortal.transform.localPosition;
            anotherPortal.GetComponent<TeleportScript>().IsRecevingPortal = true;
            IsRecevingPortal = true;
            player.GetComponent<NavMeshAgent>().transform.localPosition = pos;
            anotherPortal.transform.localPosition = player.transform.localPosition;
            
            player.GetComponent<NavMeshAgent>().enabled = true;
            StartCoroutine(WaitFewSeconds());
            teleportSound.PlayOneShot(teleportSoundClip);
        }
    }

    private IEnumerator WaitFewSeconds()
    {
        yield return new WaitForSeconds(5);

        anotherPortal.GetComponent<TeleportScript>().IsRecevingPortal = false;
        IsRecevingPortal = false;
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

}