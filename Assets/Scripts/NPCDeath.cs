using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeath : MonoBehaviour
{
    private GameObject gravestone;
    private AudioClip clip;
    private bool deathFlag;
    private int counter;

    void Start()
    {
        var random = new System.Random();
        deathFlag = false;
        gravestone = Resources.Load<GameObject>("Prefabs/Gravestone");
        clip = Resources.Load<AudioClip>("Sounds/NPCDeathSounds/Death" + random.Next(1, 11));
    }

    void Update()
    {
        if (gameObject.GetComponentInChildren<HpBar>().CurrentHeath < 0.01 && !deathFlag)
        {
            deathFlag = true;
            var obj = Instantiate(gravestone, gameObject.transform.position, Quaternion.identity);
            var source = obj.AddComponent<AudioSource>();
            source.enabled = true;
            source.volume = 0.02f;
            source.PlayOneShot(clip);
            gameObject.SetActive(false);
        }
    }
}