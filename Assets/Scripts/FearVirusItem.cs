using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class FearVirusItem : MonoBehaviour
{
    public float radius = 5f;
    public float rippleInterval = 1f;
    public int maxRippleCount = 5;
    public float infectionDuration = 6f;

    private GameObject infectedNPC;
    private GameObject[] npcs;
    private Collider2D[] colliders;
    private System.Random random;
    private List<AudioClip> sickSounds;
    private AudioMixer Mixer { get; set; }
    private int soundsCount;

    void Start()
    {
        soundsCount = 10;
        sickSounds = new List<AudioClip>();
        for (var i = 1; i < soundsCount + 1; i++)
        {
            var sickSoundClip = Resources.Load<AudioClip>("Sounds/SickSounds/Sick" + i.ToString());
            sickSounds.Add(sickSoundClip);
        }

        random = new System.Random();
        Mixer = Resources.Load<AudioMixer>("AudioMixer");
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        colliders = new Collider2D[npcs.Length];
        for (var i = 0; i < npcs.Length; i++)
            colliders[i] = npcs[i].GetComponent<Collider2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectVirusArea(Input.mousePosition, true, null);
        }
    }

    private void SelectVirusArea(Vector3 inputSpawnpoint, bool chooseByMouse, GameObject npc)
    {
        var npcCount = 0;
        var radius = 3;

        var spawnPoint = new Vector3();
        if (chooseByMouse)
            spawnPoint = Camera.main.ScreenToWorldPoint(inputSpawnpoint);
        else
        {
            spawnPoint = inputSpawnpoint;
            radius = 5;
        }
        spawnPoint.z = 0;


        var colliderInCircle = Physics2D.OverlapCircleAll(spawnPoint, radius);
        foreach(var collider in colliderInCircle)
        {
            if (collider.gameObject.tag == "NPC" && collider.gameObject != npc)
            {
                if (collider.gameObject.GetComponentInChildren<HpBar>().isInfected == false) 
                    npcCount++;

                SprayVirus(collider);
            }
        }

        if (npcCount > 0)
        {
            var soundSpeaker = new GameObject();
            var sickSound = soundSpeaker.AddComponent<AudioSource>();
            sickSound.outputAudioMixerGroup = Mixer.FindMatchingGroups("Master")[0];
            sickSound.PlayOneShot(sickSounds[random.Next(0, soundsCount)]);
        }
    }

    private void SprayVirus(Collider2D other)
    {
        Debug.Log("Spray virus on the " + other.ToString());
        other.GetComponentInChildren<HpBar>().isInfected = true;
        HpBar.infectedNPCs.Add(other.gameObject);
        StartCoroutine(VirusDurationCoroutine(other.gameObject));
        StartCoroutine(RippleEffectCoroutine(other.gameObject));
    }

    private IEnumerator VirusDurationCoroutine(GameObject npc)
    {
        if (!npc.IsDestroyed())
        {
            yield return new WaitForSeconds(infectionDuration);
            if (!npc.IsDestroyed())
            {
                npc.GetComponentInChildren<HpBar>().isInfected = false;
                npc.GetComponentInChildren<HpBar>().currentRippleCount = 0;
                HpBar.infectedNPCs.Remove(npc);
                var particle = npc.transform.GetChild(1).gameObject;
                particle.SetActive(false);
            }
        }
    }

    private IEnumerator RippleEffectCoroutine(GameObject npc)
    {
        while (!npc.IsDestroyed() && npc.GetComponentInChildren<HpBar>().currentRippleCount < maxRippleCount && npc.GetComponentInChildren<HpBar>().isInfected)
        {
            yield return new WaitForSeconds(rippleInterval);

            if (!npc.IsDestroyed() && npc.GetComponentInChildren<HpBar>().isInfected)
            {

                SelectVirusArea(npc.transform.position, false, npc);
                var particle = npc.transform.GetChild(1).gameObject;
                particle.SetActive(true);
                npc.GetComponentInChildren<HpBar>().currentRippleCount++;

                //Instantiate(particle, npc.transform.position, Quaternion.identity);
                //npc.GetComponentInChildren<HpBar>().ChangeHealth(-2.5f);

                npc.GetComponentInChildren<HpBar>().ChangeHealth(-5f);

                //if (npc.GetComponentInChildren<HpBar>().currentRippleCount == maxRippleCount)
                //{
                //    npc.GetComponentInChildren<HpBar>().isInfected = false;
                //    particle.SetActive(false);
                //}
            }
        }
    }

    private IEnumerator WaitFor(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}